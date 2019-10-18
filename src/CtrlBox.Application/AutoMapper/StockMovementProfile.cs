using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.AutoMapper
{
    public class StockMovementProfile : Profile
    {
        public StockMovementProfile()
        {
            CreateMap<StockMovement, StockMovementVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.StockType, opts => opts.MapFrom(src =>
                   Enum.GetName(typeof(EStockType), src.StockType)));

            CreateMap<StockMovementVM, StockMovement>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                .AfterMap((src, dest) => dest.Init());
        }
        }
}
