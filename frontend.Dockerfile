# Install dependencies only when needed
FROM node:16-alpine AS deps
# Check https://github.com/nodejs/docker-node/tree/b4117f9333da4138b03a546ec926ef50a31506c3#nodealpine to understand why libc6-compat might be needed.
RUN apk add --no-cache libc6-compat
WORKDIR /app

# If using npm with a `package-lock.json` comment out above and use below instead
COPY frontend/package.json frontend/package-lock.json ./ 
RUN npm ci

# Copy over the backend to build the nswag defined client classes.
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS client_build
WORKDIR /src/frontend
COPY /frontend .

WORKDIR /src/backend
COPY ["backend/Web/Web.csproj", "Web/"]
COPY ["backend/Application/Application.csproj", "Application/"]
COPY ["backend/Domain/Domain.csproj", "Domain/"]
COPY ["backend/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Web/Web.csproj"
COPY /backend .
WORKDIR "/src/backend/Web"
RUN dotnet build "Web.csproj" -c Caprover -o /app/build


# Rebuild the source code only when needed
FROM node:16-alpine AS builder
WORKDIR /app
COPY --from=deps /app/node_modules ./node_modules
COPY --from=client_build /src/frontend .
ARG NEXT_PUBLIC_API_URL=${NEXT_PUBLIC_API_URL}
ENV NEXT_PUBLIC_API_URL=${NEXT_PUBLIC_API_URL}
RUN echo "BUILDER APIURL: ${NEXT_PUBLIC_API_URL}"
RUN npm run build

# Production image, copy all the files and run next
FROM node:16-alpine AS runner
WORKDIR /app
ARG NEXT_PUBLIC_API_URL=${NEXT_PUBLIC_API_URL}
ENV NEXT_PUBLIC_API_URL=${NEXT_PUBLIC_API_URL}
RUN echo "RUNNER APIURL: ${NEXT_PUBLIC_API_URL}"
ENV NODE_ENV production

RUN addgroup --system --gid 1001 nodejs
RUN adduser --system --uid 1001 nextjs

# You only need to copy next.config.js if you are NOT using the default configuration
# COPY --from=builder /app/next.config.js ./
COPY --from=builder /app/public ./public
COPY --from=builder /app/package.json ./package.json

# Automatically leverage output traces to reduce image size 
# https://nextjs.org/docs/advanced-features/output-file-tracing
COPY --from=builder --chown=nextjs:nodejs /app/.next/standalone ./
COPY --from=builder --chown=nextjs:nodejs /app/.next/static ./.next/static

USER nextjs

EXPOSE 3000
ENV PORT 3000

CMD ["node", "server.js"]