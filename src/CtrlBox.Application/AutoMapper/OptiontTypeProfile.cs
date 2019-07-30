using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Entities;
using System;

namespace CtrlBox.Application.AutoMapper
{
   public class OptiontTypeProfile : Profile
    {
        public OptiontTypeProfile()
        {
            CreateMap<OptiontType, OptiontTypeVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id))
                 .ForMember(dest => dest.EClientType, opts => opts.MapFrom(src =>
                                   Enum.GetName(typeof(EClientType), src.EClientType)));

            CreateMap<OptiontTypeVM, OptiontType>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                .AfterMap((src, dest) => dest.Init());
        }
    }
}
