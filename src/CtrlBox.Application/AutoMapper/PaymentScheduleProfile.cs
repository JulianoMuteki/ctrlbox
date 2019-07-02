using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
   public class PaymentScheduleProfile: Profile
    {
        public PaymentScheduleProfile()
        {
            CreateMap<PaymentSchedule, PaymentScheduleVM>()
               .ForMember(dest => dest.DT_RowId,
                         opts => opts.MapFrom(src => src.Id));

            CreateMap<PaymentScheduleVM, PaymentSchedule>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId))
                  .AfterMap((src, dest) => dest.Init());
        }
    }
}
