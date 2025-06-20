using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Todo.Domain.Entities.Identity;

namespace Todo.Repo.identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<Appuser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var newuser = new Appuser()
                {
                    DisplayName = "Mohamed Hesham",
                    Email = "mohamedelhanafy290@gmail.com",
                    UserName = "mohamedelhanafy290"
                };
                var result = await userManager.CreateAsync(newuser, "Pa$$w0rd");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newuser, "Admin");
                }
            }
        }
    }
}