﻿using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class PaymentVM : ViewModelBase
    {
        public decimal TotalValueSale { get; set; }
        public decimal RemainingValue { get; set; }

        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCashPayment { get; set; }
        public int NumberParcels { get; set; }

        public Guid SaleID { get; set; }
        public SaleVM Sale { get; set; }

        public IList<PaymentScheduleVM> PaymentsSchedules { get; set; }

        public PaymentVM()
        {
            this.PaymentsSchedules = new List<PaymentScheduleVM>();
        }
    }
}
