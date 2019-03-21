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
            });
        }
    }
}
