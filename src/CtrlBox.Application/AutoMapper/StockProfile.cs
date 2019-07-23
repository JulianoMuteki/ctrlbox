using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application.AutoMapper
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<Stock, StockVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id));

            CreateMap<StockVM, Stock>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId));
        }
    }
}
