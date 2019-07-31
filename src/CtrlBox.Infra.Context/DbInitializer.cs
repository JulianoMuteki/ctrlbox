using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Identity;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

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
               // SeedDataBox(serviceProvider);
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
            if (userManager.FindByNameAsync("juliano.pestili@outlook.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "juliano.pestili@outlook.com";
                user.Email = "juliano.pestili@outlook.com";
                user.PhoneNumber = "(19) 99999-8888";
                user.FirstName = "Admin";
                user.LastName = "User";

                IdentityResult result = userManager.CreateAsync(user, "Pa$$w0rd").Result;

                if (result.Succeeded)
                {
                    result = userManager.AddToRoleAsync(user, RoleAuthorize.Admin.ToString()).Result;
                }
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            foreach (var roleName in Enum.GetNames(typeof(RoleAuthorize)))
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    ApplicationRole role = new ApplicationRole();
                    role.Name = roleName;
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;

                    //if (RoleAuthorize.Admin.ToString() == roleName)
                    //    roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.DefaultPermission, PolicyTypes.DeliveryPolicy.ExecuteDelivery)).Wait();
                }
            }
        }

        public static void SeedData(IServiceProvider serviceProvider)
        {
            var clientService = serviceProvider.GetRequiredService<IClientApplicationService>();
            if (clientService.GetAll().Count == 0)
            {
                AddressVM addressVM = new AddressVM();
                addressVM.CEP = "13276-130";
                addressVM.Estate = "SP";
                addressVM.Street = "Av. Onze de Agosto";
                addressVM.Number = "623";
                addressVM.City = "Valinhos";
                addressVM.District = "Centro";
                addressVM.Reference = "";
                var addressService = serviceProvider.GetRequiredService<IAddressApplicationService>();
                addressVM = addressService.Add(addressVM);

                for (int i = 0; i < 50; i++)
                {
                    ClientVM client = new ClientVM();
                    client.Name = $"Cliente - {i}";
                    client.Contact = $"Contanto - {i}";
                    client.SaleIsFinished = false;
                    client.Phone = "19-99999-9999";
                    client.Address = addressVM;
                    clientService.Add(client);
                }

                var productService = serviceProvider.GetRequiredService<IProductApplicationService>();

                var routeService = serviceProvider.GetRequiredService<IRouteApplicationService>();
                for (int i = 0; i < 10; i++)
                {
                    RouteVM route = new RouteVM();
                    route.Name = $"Rota - {i}";
                    route.KmDistance = 100 + 1;
                    route.Truck = $"Costallation - {i}";
                    route.HasOpenDelivery = false;
                    routeService.Add(route);
                }

                var paymentService = serviceProvider.GetRequiredService<IPaymentApplicationService>();

                PaymentMethodVM paymentMethodVM = new PaymentMethodVM();
                paymentMethodVM.MethodName = "Check";
                paymentMethodVM.Descrition = "Payment with check";
                paymentService.AddPaymentMethod(paymentMethodVM);
                paymentMethodVM.MethodName = "Money";
                paymentMethodVM.Descrition = "Payment with Money";
                paymentService.AddPaymentMethod(paymentMethodVM);
                paymentMethodVM.MethodName = "Bank bill";
                paymentMethodVM.Descrition = "Payment with Bank bill";
                paymentService.AddPaymentMethod(paymentMethodVM);
                paymentMethodVM.MethodName = "Credit";
                paymentMethodVM.Descrition = "Payment with Credit";
                paymentService.AddPaymentMethod(paymentMethodVM);
            }
        }
    }
}
