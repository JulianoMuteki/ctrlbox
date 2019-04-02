﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class SaleVM: ViewModelBase
    {
        public Guid ClientID { get; set; }
        public Guid DeliveryID { get; set; }

        public decimal ReceivedValue { get; set; }
        public decimal ForwardValue { get; set; }
        public int TotalReturnedBoxes { get; set; }

        public virtual ClientVM Client { get; set; }
        public virtual DeliveryVM Delivery { get; set; }

        public ICollection<CheckVM> Checks { get; set; }
        public ICollection<DeliveryProductVM> DeliveriesProducts { get; set; }
        public ICollection<SaleProductVM> SalesProducts { get; set; }

        public SaleVM()
        {
            this.SalesProducts = new HashSet<SaleProductVM>();
            this.Checks = new HashSet<CheckVM>();
            this.DeliveriesProducts = new HashSet<DeliveryProductVM>();
        }
    }
}
