using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.AutoMapper
{
    class CheckProfile : Profile
    {
        public CheckProfile()
        {
            CreateMap<Check, CheckVM>();
            CreateMap<CheckVM, Check>();
        }
    }
}