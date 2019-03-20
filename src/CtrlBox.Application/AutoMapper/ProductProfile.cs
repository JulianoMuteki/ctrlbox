using AutoMapper;
using CtrlBox.Application.ViewModels;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id));
        }
    }
}
