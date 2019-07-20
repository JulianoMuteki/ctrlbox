﻿using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.AutoMapper
{
    public class ProductItemProfile : Profile
    {
        public ProductItemProfile()
        {
            CreateMap<ProductItem, ProductItemVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id));

            CreateMap<ProductItemVM, ProductItem>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                .AfterMap((src, dest) => dest.Init());
        }
    }
}
