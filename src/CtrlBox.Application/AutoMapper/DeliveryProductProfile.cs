using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class DeliveryProductProfile : Profile
    {
        public DeliveryProductProfile()
        {
            CreateMap<DeliveryDetail, DeliveryDetailVM>();

            CreateMap<DeliveryDetailVM, DeliveryDetail>();
        }
    }
}
