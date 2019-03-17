using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Infra.Repository;
using CtrlBox.Infra.Repository.Common;
using CtrlBox.Infra.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CtrlBox.CrossCutting.Ioc
{
    public class InfraBootStrapperModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //helper service
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Infra - Data
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
