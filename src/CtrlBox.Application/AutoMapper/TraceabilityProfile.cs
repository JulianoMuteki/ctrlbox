using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class TraceabilityProfile : Profile
    {
        public TraceabilityProfile()
        {
            CreateMap<BoxTracking, TraceabilityVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id));

            CreateMap<TraceabilityVM, BoxTracking>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                .AfterMap((src, dest) => dest.Init());
        }
    }
}