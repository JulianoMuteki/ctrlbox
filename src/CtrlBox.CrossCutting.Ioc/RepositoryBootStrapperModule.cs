using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Infra.Repository;
using CtrlBox.Infra.Repository.Common;
using Microsoft.Extensions.DependencyInjection;

namespace CtrlBox.CrossCutting.Ioc
{
    public class RepositoryBootStrapperModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Infra - Data
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //helper service
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
