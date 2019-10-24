using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CtrlBox.CrossCutting.Ioc
{
    public class InfraBootStrapperModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //helper service
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<NotificationContext, NotificationContext>();
            
        }
    }
}
