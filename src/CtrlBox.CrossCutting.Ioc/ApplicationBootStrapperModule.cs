using CtrlBox.Application;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;

namespace CtrlBox.CrossCutting.Ioc
{
    public class ApplicationBootStrapperModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IProductApplicationService, ProductApplicationService>();
            services.AddScoped<IClientApplicationService, ClientApplicationService>();
            services.AddScoped<IRouteApplicationService, RouteApplicationService>();
            services.AddScoped<IDeliveryApplicationService, DeliveryApplicationService>();
            services.AddScoped<ISaleApplicationService, SaleApplicationService>();
            services.AddScoped<ISecurityApplicationService, SecurityApplicationService>();
            services.AddScoped<IPaymentApplicationService, PaymentApplicationService>();
            services.AddScoped<IAddressApplicationService, AddressApplicationService>();
            services.AddScoped<IBoxApplicationService, BoxApplicationService>();
            services.AddScoped<IConfigurationApplicationService, ConfigurationApplicationService>();

            services.AddScoped<ICustomEmailSender, CustomEmailSender>();

        }
    }
}
