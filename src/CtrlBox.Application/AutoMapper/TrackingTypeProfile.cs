using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using System;

namespace CtrlBox.Application.AutoMapper
{
   public class TrackingTypeProfile : Profile
    {
        public TrackingTypeProfile()
        {
            CreateMap<TrackingType, TrackingTypeVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id))
             .ForMember(dest => dest.TrackType, opts => opts.MapFrom(src =>
                   Enum.GetName(typeof(TrackType), src.TrackType)));

            CreateMap<TrackingTypeVM, TrackingType>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))

                 .AfterMap((src, dest) => dest.Init());
        }
    }
}
