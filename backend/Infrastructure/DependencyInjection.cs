using Application.Common.Interfaces;
using Domain.Common;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                    var server = Environment.GetEnvironmentVariable("DB_SERVER");
                    var port = Environment.GetEnvironmentVariable("DB_PORT");
                    var database = Environment.GetEnvironmentVariable("DATABASE");
                    var userId = Environment.GetEnvironmentVariable("POSTGRES_USER");
                    var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql($"Server={server};Port={port};Database={database};User Id={userId};Password={password}",
                        b => { 
                            b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                            b.UseNetTopologySuite();
                            }
                        ));
                }
                else
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(
                            configuration.GetConnectionString("Postgresql"),
                            b =>
                            {
                                b.UseNetTopologySuite();
                                b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                            }));
                }
                //TODO: Add timetracking as a trancient dependency here.
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer().AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
