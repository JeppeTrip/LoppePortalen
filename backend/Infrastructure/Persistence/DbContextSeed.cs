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
            List<IdentityRole> defaultRoles = new List<IdentityRole>()
            {
                new IdentityRole("Administrator"),
                new IdentityRole("ApplicationUser")
            };

            defaultRoles.ForEach(async role =>
            {
                if (roleManager.Roles.All(r => r.Name != role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            });

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { "Administrator", "ApplicationUser" });

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

        public static async Task SeedDefaultItemCategories(IApplicationDbContext context)
        {
            List<string> categories = new List<string>()
            {
                "Electronics",
                "Furniture",
                "Clothing",
                "Gardening",
                "Antiques",
                "Music",
                "Instruments",
                "Games",
                "Toys",
                "Kids"
            };

            categories.ForEach(x =>
            {
                var item = context.ItemCategories.FirstOrDefault(ic => ic.Name.Equals(x));
                if (item == null)
                    context.ItemCategories.Add(new Category() { Name = x });
            });

            await context.SaveChangesAsync(CancellationToken.None);

        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
        }
    }
}
