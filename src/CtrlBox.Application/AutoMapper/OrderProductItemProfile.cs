using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class OrderProductItemProfile : Profile
    {
        public OrderProductItemProfile()
        {
            CreateMap<OrderProductItem, OrderProductItemVM>();
            CreateMap<OrderProductItemVM, OrderProductItem>();
        }
    }
}
