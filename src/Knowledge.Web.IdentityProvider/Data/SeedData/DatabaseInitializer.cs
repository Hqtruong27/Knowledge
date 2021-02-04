using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge.Web.IdentityProvider.Data.SeedData
{
    public class DatabaseInitializer
    {
        private readonly UserManager<User> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private const string AdminRole = "Admin";
        private const string UserRole = "Member";
        private static string NewGuild => Guid.NewGuid().ToString();
        public DatabaseInitializer(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            #region 1: Role
            if (!RoleManager.Roles.Any())
            {
                await RoleManager.CreateAsync(new IdentityRole
                {
                    Id = AdminRole,
                    Name = AdminRole,
                    NormalizedName = AdminRole.ToUpper()
                });
            }
            #endregion

            #region 2: User
            if (!UserManager.Users.Any())
            {
                var result = await UserManager.CreateAsync(new User
                {
                    Id = NewGuild,
                    UserName = "hqtruong27",
                    FirstName = "Truong",
                    LastName = "Hoang",
                    Email = "hqtruong27@gmail.com",
                    LockoutEnabled = false
                }, "Truong2207");
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByNameAsync("hqtruong27");
                    await UserManager.AddToRoleAsync(user, AdminRole);
                }
            }
            #endregion
        }
    }
}
