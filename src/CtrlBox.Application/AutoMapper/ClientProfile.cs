using AutoMapper;
using CtrlBox.Application.ViewModels;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class ClientProfile: Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientVM>()
                 .ForMember(dest => dest.DT_RowId,
                           opts => opts.MapFrom(src => src.Id));

            CreateMap<ClientVM, Client>()
                   .ForMember(dest => dest.Id,
                             opts => opts.MapFrom(src => src.DT_RowId));
        }
    }
}
