using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class BoxTypeProfile : Profile
    {
        public BoxTypeProfile()
        {
            CreateMap<BoxType, BoxTypeVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id));

            CreateMap<BoxTypeVM, BoxType>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId));
        }
    }
}
