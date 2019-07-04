using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressVM>()
                    .ForMember(dest => dest.DT_RowId,
                        opts => opts.MapFrom(src => src.Id));

            CreateMap<AddressVM, Address>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                            .AfterMap((src, dest) => dest.Init());
        }
    }
}
