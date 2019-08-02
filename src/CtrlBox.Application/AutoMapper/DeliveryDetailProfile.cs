using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class DeliveryDetailProfile : Profile
    {
        public DeliveryDetailProfile()
        {
            CreateMap<DeliveryDetail, DeliveryDetailVM>()
                                .ForMember(dest => dest.DT_RowId,
                        opts => opts.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Client, opt => opt.Ignore());


            CreateMap<DeliveryDetailVM, DeliveryDetail>()
                            .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                            .AfterMap((src, dest) => dest.Init());
        }
    }
}
