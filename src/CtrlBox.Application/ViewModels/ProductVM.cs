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

    public class StockProductVM
    {
        public int Amount { get; set; }
        public Guid ProductID { get; set; }
        public Guid StockID { get; set; }

        public string Name { get; set; }
    }
}
