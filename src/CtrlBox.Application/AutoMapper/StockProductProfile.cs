using AutoMapper;
using CtrlBox.Application.ViewModels;
using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
