using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Data
{
    public static class SeededData
    {
        public async static Task Seed(UserManager<IdentityUser> userManeger, RoleManager<IdentityRole> roleManeger)
        {
            await SeedRoles(roleManeger);
            await SeedUsers(userManeger);
        }
        private async static Task SeedUsers(UserManager<IdentityUser> userManeger)
        {
            if (await userManeger.FindByEmailAsync("admin@gmail.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };
                var result = await userManeger.CreateAsync(user, "12345678");
                if (result.Succeeded)
                {
                    await userManeger.AddToRoleAsync(user, "Administrator");
                }
            }


            if (await userManeger.FindByEmailAsync("customer@gmail.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "Customer@gmail.com",
                    Email = "customer@gmail.com"
                };
                var result = await userManeger.CreateAsync(user, "12345678");
                if (result.Succeeded)
                {
                    await userManeger.AddToRoleAsync(user, "NotAdministrator");
                }
            }

          
        }
        private async static Task SeedRoles(RoleManager<IdentityRole> roleManeger)
        {
            if (!await roleManeger.RoleExistsAsync("Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                await roleManeger.CreateAsync(role);
            }

            if (!await roleManeger.RoleExistsAsync("NotAdministrator"))
            {
                var role = new IdentityRole
                {
                    Name = "NotAdministrator"
                };
                await roleManeger.CreateAsync(role);
            }
        }
    }
}
