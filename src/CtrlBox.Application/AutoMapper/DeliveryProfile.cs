using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
   public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            CreateMap<Order, OrderVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.RouteName,
                          opts => opts.MapFrom(src => src.Route.Name))
                .ForMember(dest => dest.UserName,
                          opts => opts.MapFrom(src => src.User.UserName));

            CreateMap<OrderVM, Order>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                 .AfterMap((src, dest) => dest.Init());
        }
    }
}
