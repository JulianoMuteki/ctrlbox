using CtrlBox.Domain.Security;
using CtrlBox.Infra.Context.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace CtrlBox.Infra.Context
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var userManager = serviceProvider.    GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.    GetRequiredService<RoleManager<ApplicationRole>>();

                SeedData    (userManager, roleManager);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }


        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("AdminUser").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "AdminUser";
                user.Email = "juliano.pestili@outlook.com";
                user.FirstName = "Admin";
                user.LastName = "User";

                IdentityResult result = userManager.CreateAsync(user, "Pa$$w0rd").Result;

                if (result.Succeeded)
                {
                    result = userManager.AddToRoleAsync(user, Role.Admin.ToString()).Result;
                }
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            foreach (var roleName in Enum.GetNames(typeof(Role)))
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    ApplicationRole role = new ApplicationRole();
                    role.Name = roleName;
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;

                    foreach (var claimName in Enum.GetNames(typeof(CRUDClaim)))
                    {
                        roleResult = roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, $"{ roleName },{ claimName }")).Result;
                    }
                }
            }          
        }
    }
}
