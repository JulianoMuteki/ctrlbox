using AutoMapper;
using CtrlBox.Application.ViewModels;
using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.AutoMapper
{
    public class ClientProductValueProfile : Profile
    {
        public ClientProductValueProfile()
        {
            CreateMap<ClientProductValue, ClientProductValueVM>();

            CreateMap<ClientProductValueVM, ClientProductValue>();
        }
    }
}
