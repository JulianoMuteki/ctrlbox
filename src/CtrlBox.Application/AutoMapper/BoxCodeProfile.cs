using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class BoxCodeProfile : Profile
    {
        public BoxCodeProfile()
        {
            CreateMap<BoxCode, BoxCodeVM>()
                 .ForMember(dest => dest.DT_RowId,
                           opts => opts.MapFrom(src => src.Id));

            CreateMap<BoxCodeVM, BoxCode>()
                   .ForMember(dest => dest.Id,
                             opts => opts.MapFrom(src => src.DT_RowId));
        }
    }
}