#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Web/Web.csproj", "Web/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Web/Web.csproj"
COPY . .
WORKDIR "/src/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

FROM base AS final
ARG ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT}
ARG DB_SERVER=${DB_SERVER}
ARG DB_PORT=${DB_PORT}
ARG DATABASE=${DATABASE}
ARG POSTGRES_USER=${POSTGRES_USER}
ARG POSTGRES_PASSWORD=${POSTGRES_PASSWORD}
ARG FRESH_MIGRATION=${FRESH_MIGRATION}
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]