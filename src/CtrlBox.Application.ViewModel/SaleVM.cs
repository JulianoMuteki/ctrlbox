﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class SaleVM: ViewModelBase
    {
        public Guid ClientID { get; set; }
        public Guid OrderID { get; set; }

        public decimal ReceivedValue { get; set; }
        public decimal ForwardValue { get; set; }
        public bool IsFinished { get; set; }

        public virtual ClientVM Client { get; set; }
        public virtual OrderVM Order { get; set; }
        public virtual PaymentVM Payment { get; set; }

        public bool HasDelivery { get; set; }

        public ICollection<SaleProductVM> SalesProducts { get; set; }

        public SaleVM()
        {
            this.SalesProducts = new HashSet<SaleProductVM>();
        }
    }
}
