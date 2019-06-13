using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleVM>()
                                .ForMember(dest => dest.DT_RowId,
                                    opts => opts.MapFrom(src => src.Id));

            // .ForMember(dest => dest.DeliveriesProducts, opt => opt.Ignore())
            //.ForMember(dest => dest.SalesProducts, opt => opt.Ignore())
            //.ForMember(dest => dest.Checks, opt => opt.Ignore());

            CreateMap<SaleVM, Sale>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                            .AfterMap((src, dest) => dest.Init());

        }
    }
}
