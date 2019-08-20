using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
   public class TrackingClientProfile : Profile
    {
        public TrackingClientProfile()
        {
            CreateMap<TrackingClient, TrackingClientVM>();

            CreateMap<TrackingClientVM, TrackingClient>();
        }
    }
}