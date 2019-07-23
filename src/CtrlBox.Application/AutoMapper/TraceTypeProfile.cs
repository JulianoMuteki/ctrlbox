using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using System;

namespace CtrlBox.Application.AutoMapper
{
   public class TraceTypeProfile : Profile
    {
        public TraceTypeProfile()
        {
            CreateMap<TraceType, TraceTypeVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id))
             .ForMember(dest => dest.TypeTrace, opts => opts.MapFrom(src =>
                   Enum.GetName(typeof(TypeTrace), src.TypeTrace)));

            CreateMap<TraceTypeVM, TraceType>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))

                 .AfterMap((src, dest) => dest.Init());


        }
    }

 
}
