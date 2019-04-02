using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
   public class StockProductProfile: Profile
    {
        public StockProductProfile()
        {
            CreateMap<StockProduct, StockProductVM>();
            CreateMap<StockProductVM, StockProduct>();
        }
    }
}
