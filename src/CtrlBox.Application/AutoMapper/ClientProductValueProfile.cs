using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class ClientProductValueProfile : Profile
    {
        public ClientProductValueProfile()
        {
            CreateMap<ClientProductValue, ClientProductValueVM>();

            CreateMap<ClientProductValueVM, ClientProductValue>();
        }
    }
}
