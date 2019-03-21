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
        }
    }
}
