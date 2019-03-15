using Microsoft.Extensions.DependencyInjection;

namespace CtrlBox.CrossCutting.Ioc
{
    public class ApplicationBootStrapperModule
    {
        public static void RegisterServices(IServiceCollection services)
        {

          //  services.AddScoped<IClientApplicationService, ClientApplicationService>();
        }
    }
}
