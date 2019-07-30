using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    class ClientOptionTypeProfile : Profile
    {
        public ClientOptionTypeProfile()
        {
            CreateMap<ClientOptionType, ClientOptionTypeVM>();
            CreateMap<ClientOptionTypeVM, ClientOptionType>();
        }
    }
}
