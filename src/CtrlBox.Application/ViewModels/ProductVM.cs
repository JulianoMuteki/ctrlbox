﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModels
{
    public class ProductVM : ViewModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
    }

   public class ClientProductValueVM
    {
        public Guid ProductID { get; set; }

        public Guid ClientID { get; set; }

        public float Price { get; set; }

    }
}
