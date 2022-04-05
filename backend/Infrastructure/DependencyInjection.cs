using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Domain.Entities;
using System;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            if (!environment.IsEnvironment("Test"))
            {
                if (environment.IsEnvironment("caprover"))
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(
                            configuration.GetConnectionString($"Server={Environment.GetEnvironmentVariable("DB_SERVER")};Port={Environment.GetEnvironmentVariable("DB_PORT")};Database={Environment.GetEnvironmentVariable("DATABASE")};User Id={Environment.GetEnvironmentVariable("POSTGRES_USER")};Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}"),
                            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
                }
                else
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(
                            configuration.GetConnectionString("Postgresql"),
                            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
                }



                //TODO: Add timetracking as a trancient dependency here.
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
