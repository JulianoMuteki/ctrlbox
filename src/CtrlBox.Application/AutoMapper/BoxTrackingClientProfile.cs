using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
   public class BoxTrackingClientProfile : Profile
    {
        public BoxTrackingClientProfile()
        {
            CreateMap<BoxTrackingClient, BoxTrackingClientVM>();

            CreateMap<BoxTrackingClientVM, BoxTrackingClient>();
        }
    }
}