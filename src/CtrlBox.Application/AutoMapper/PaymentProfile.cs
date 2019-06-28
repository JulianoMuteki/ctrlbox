using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class PaymentProfile: Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentVM>()
                        .ForMember(dest => dest.DT_RowId,
                             opts => opts.MapFrom(src => src.Id));

            CreateMap<PaymentVM, Payment>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                 .AfterMap((src, dest) => dest.Init());
        }
    }
}
