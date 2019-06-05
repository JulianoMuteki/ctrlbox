using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.AutoMapper
{
    public class SaleProductProfile : Profile
    {
        public SaleProductProfile()
        {
            CreateMap<SaleProduct, SaleProductVM>();
            CreateMap<SaleProductVM, SaleProduct>();
        }
    }
}
