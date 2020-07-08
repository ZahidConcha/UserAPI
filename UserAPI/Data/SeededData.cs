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
                    UserName = "admin",
                    Email = "admin@gmail.com"
                };
                var result = await userManeger.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManeger.AddToRoleAsync(user, "Admin");
                }
            }


            if (await userManeger.FindByEmailAsync("customer@gmail.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "Customer",
                    Email = "customer@gmail.com"
                };
                var result = await userManeger.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManeger.AddToRoleAsync(user, "NotAdmin");
                }
            }

          
        }
        private async static Task SeedRoles(RoleManager<IdentityRole> roleManeger)
        {
            if (!await roleManeger.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                await roleManeger.CreateAsync(role);
            }

            if (!await roleManeger.RoleExistsAsync("NotAdmin"))
            {
                var role = new IdentityRole
                {
                    Name = "NotAdmin"
                };
                await roleManeger.CreateAsync(role);
            }
        }
    }
}
