using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Entities;
using System;

namespace CtrlBox.Application.AutoMapper
{
    public class ProductItemProfile : Profile
    {
        public ProductItemProfile()
        {
            CreateMap<ProductItem, ProductItemVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src =>
                   Enum.GetName(typeof(EProductItemStatus), src.Status)));

            CreateMap<ProductItemVM, ProductItem>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                .AfterMap((src, dest) => dest.Init());
        }
    }
}
