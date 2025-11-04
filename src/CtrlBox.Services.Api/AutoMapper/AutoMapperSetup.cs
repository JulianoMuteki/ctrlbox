using AutoMapper;
using CtrlBox.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;


namespace CtrlBox.Services.Api.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class AutoMapperSetup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
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
