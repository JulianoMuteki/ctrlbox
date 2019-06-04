using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleVM>();
                // .ForMember(dest => dest.DeliveriesProducts, opt => opt.Ignore())
                //.ForMember(dest => dest.SalesProducts, opt => opt.Ignore())
                //.ForMember(dest => dest.Checks, opt => opt.Ignore());

            CreateMap<SaleVM, Sale>();
        }
    }
}
