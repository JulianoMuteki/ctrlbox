using CtrlBox.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace CtrlBox.UI.Web.Mappers
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(Assembly.GetEntryAssembly());

            // Registering Mappings automatically only works if the 
            // Automapper Profile classes are in ASP.NET project
            AutoMapperConfig.RegisterMappings();
        }
    }
}
