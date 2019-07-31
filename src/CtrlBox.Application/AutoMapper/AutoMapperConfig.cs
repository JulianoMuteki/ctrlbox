using AutoMapper;

namespace CtrlBox.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new ProductProfile());
                cfg.AddProfile(new RouteProfile());
                cfg.AddProfile(new DeliveryProfile());
                cfg.AddProfile(new ClientProductValueProfile());
                cfg.AddProfile(new DeliveryProductProfile());
                cfg.AddProfile(new SaleProfile());
                cfg.AddProfile(new SaleProductProfile());
            });
        }
    }
}
