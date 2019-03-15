using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Infra.Repository;
using CtrlBox.Infra.Repository.Common;
using Microsoft.Extensions.DependencyInjection;

namespace CtrlBox.CrossCutting.Ioc
{
    public class InfraBootStrapperModule
    {
        public static void RegisterServices(IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
