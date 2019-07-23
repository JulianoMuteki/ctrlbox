using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
   public class TraceabilityClientProfile : Profile
    {
        public TraceabilityClientProfile()
        {
            CreateMap<TraceabilityClient, TraceabilityClientVM>();

            CreateMap<TraceabilityClientVM, TraceabilityClient>();
        }
    }
}