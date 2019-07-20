using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomersProductsValues, opt => opt.Ignore())
                .ForMember(dest => dest.DeliveriesProducts, opt => opt.Ignore())
                .ForMember(dest => dest.StocksProducts, opt => opt.Ignore())
                .ForMember(dest => dest.SalesProducts, opt => opt.Ignore());

            CreateMap<ProductVM, Product>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                           .AfterMap((src, dest) => dest.Init());
        }
    }
}
