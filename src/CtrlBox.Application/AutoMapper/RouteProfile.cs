using AutoMapper;
using CtrlBox.Application.ViewModels;
using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.AutoMapper
{
    public class RouteProfile : Profile
    {
        public RouteProfile()
        {
            CreateMap<Route, RouteVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id));

            CreateMap<RouteVM, Route>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId));
        }
    }
}
