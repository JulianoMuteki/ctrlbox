using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
   public class DeliveryBoxProfile : Profile
    {
        public DeliveryBoxProfile()
        {
            CreateMap<DeliveryBox, DeliveryBoxVM>();
            CreateMap<DeliveryBoxVM, DeliveryBox>();
        }
    }
}
