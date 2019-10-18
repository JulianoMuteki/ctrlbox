using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.AutoMapper
{
    class StockProfiler : Profile
    {
        public StockProfiler()
        {
            CreateMap<Stock, StockVM>()
                 .ForMember(dest => dest.DT_RowId,
                           opts => opts.MapFrom(src => src.Id));

            CreateMap<StockVM, Stock>()
                   .ForMember(dest => dest.Id,
                             opts => opts.MapFrom(src => src.DT_RowId))
                   .AfterMap((src, dest) => dest.Init());
        }
    }
}
