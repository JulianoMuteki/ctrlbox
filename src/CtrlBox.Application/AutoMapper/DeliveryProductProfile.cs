using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class DeliveryProductProfile : Profile
    {
        public DeliveryProductProfile()
        {
            CreateMap<DeliveryProduct, DeliveryProductVM>();

            CreateMap<DeliveryProductVM, DeliveryProduct>();
        }
    }
}
