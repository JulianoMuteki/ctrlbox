using AutoMapper;
using CtrlBox.Application.ViewModels;
using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.AutoMapper
{
   public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            CreateMap<Delivery, DeliveryVM>()
                .ForMember(dest => dest.DT_RowId,
                          opts => opts.MapFrom(src => src.Id));

            CreateMap<DeliveryVM, Delivery>()
                .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.DT_RowId));
        }
    }
}
