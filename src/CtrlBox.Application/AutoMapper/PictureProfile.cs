using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class PictureProfile : Profile
    {
        public PictureProfile()
        {
            CreateMap<Picture, PictureVM>()
                                .ForMember(dest => dest.DT_RowId,
                                    opts => opts.MapFrom(src => src.Id));

            CreateMap<PictureVM, Picture>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                            .AfterMap((src, dest) => dest.Init());
        }
    }
}
