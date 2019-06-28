using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class PaymentMethodProfile: Profile
    {
        public PaymentMethodProfile()
        {
            CreateMap<PaymentMethod, PaymentMethodVM>()
            .ForMember(dest => dest.DT_RowId,
                 opts => opts.MapFrom(src => src.Id));

            CreateMap<PaymentMethodVM, PaymentMethod>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId));
        }
    }
}
