using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
   public class TraceTypeProfile : Profile
    {
        public TraceTypeProfile()
        {
            CreateMap<TraceType, TraceTypeVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id))
                 .ForMember(dest => dest.TypeTrace,
                            opts => opts.MapFrom(src => src.TypeTrace));

            CreateMap<TraceTypeVM, TraceType>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                .ForMember(dest => dest.TypeTrace,
                            opts => opts.MapFrom(src => src.TypeTrace));


        }
    }
}
