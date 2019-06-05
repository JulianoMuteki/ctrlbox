using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Security;
using CtrlBox.Infra.Context.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace CtrlBox.Infra.Context
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                SeedData(userManager, roleManager);
                SeedData(serviceProvider);
            }
            catch (Exception ex)
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

        public static void SeedData(IServiceProvider serviceProvider)
        {
            var clientService = serviceProvider.GetRequiredService<IClientApplicationService>();
            if (clientService.GetAll().Count == 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    Client client = new Client();
                    client.Name = $"Cliente - {i}";
                    client.Address = $"Rua José Nº {i}";
                    client.BalanceDue = 0;
                    client.Contact = $"Contanto - {i}";
                    client.IsDelivery = false;
                    client.Phone = "19-99999-9999";
                    client.QuantityBoxes = 0;
                    clientService.Add(client);
                }

                var productService = serviceProvider.GetRequiredService<IProductApplicationService>();

                for (int i = 0; i < 5; i++)
                {
                    Product product = new Product();
                    product.Description = $"Descrição nanica {i}";
                    product.Name = $"Banana nanica - {i}";
                    product.Weight = i;
                    productService.Add(product);
                }
                for (int i = 0; i < 5; i++)
                {
                    Product product = new Product();
                    product.Description = $"Descrição maçã {i}";
                    product.Name = $"Banana maçã - {i}";
                    product.Weight = i;
                    productService.Add(product);
                }
                for (int i = 0; i < 5; i++)
                {
                    Product product = new Product();
                    product.Description = $"Descrição prata {i}";
                    product.Name = $"Banana prata - {i}";
                    product.Weight = i;
                    productService.Add(product);
                }

                var routeService = serviceProvider.GetRequiredService<IRouteApplicationService>();
                for (int i = 0; i < 10; i++)
                {
                    Route route = new Route();
                    route.Name = $"Rota - {i}";
                    route.KmDistance = 100 + 1;
                    route.Truck = $"Costallation - {i}";
                    route.HasOpenDelivery = false;
                    routeService.Add(route);
                }

                var productApplicationService = serviceProvider.GetRequiredService<IProductApplicationService>();
                productApplicationService.AddStock(100000);
            }
        }
    }
}
