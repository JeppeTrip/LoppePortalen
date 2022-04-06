module.exports = {
    experimental: {
      outputStandalone: true,
    },
    async rewrites() {
      return {
        fallback: [
          // These rewrites are checked after both pages/public files
          // and dynamic routes are checked
          {
            source: '/:path*',
            destination: `/error`,
          },
        ],
      }
    },
  }