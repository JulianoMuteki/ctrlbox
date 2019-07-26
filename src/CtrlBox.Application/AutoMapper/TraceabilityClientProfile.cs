using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
   public class TraceabilityClientProfile : Profile
    {
        public TraceabilityClientProfile()
        {
            CreateMap<BoxTrackingClient, BoxTrackingClientVM>();

            CreateMap<BoxTrackingClientVM, BoxTrackingClient>();
        }
    }
}