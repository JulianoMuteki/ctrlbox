using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    class OrderBoxProfile : Profile
    {
        public OrderBoxProfile()
        {
            CreateMap<OrderBox, OrderBoxVM>();
            CreateMap<OrderBoxVM, OrderBox>();
        }
    }
}
