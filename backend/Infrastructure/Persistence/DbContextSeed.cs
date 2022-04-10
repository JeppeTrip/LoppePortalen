using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class DbContextSeed
    {
        public static async Task SeedDefaultUserAsync(IApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });

                var newUser = new User()
                {
                    IdentityId = administrator.Id,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = administrator.Email,
                    DateOfBirth = DateTimeOffset.UtcNow,
                    Phone = "",
                    Country = "Denmark"
                };
                context.UserInfo.Add(newUser);
                await context.SaveChangesAsync(CancellationToken.None);
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
        }
    }
}
