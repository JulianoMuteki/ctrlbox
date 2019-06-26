using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class PaymentVM : ViewModelBase
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public int NumberParcels { get; set; }
        public decimal RemainingValue { get; set; }

        public Guid SaleID { get; set; }
        public Guid PaymentMethodID { get; set; }
        public Guid? PaymentScheduleID { get; set; }

        public SaleVM SaleVM { get; set; }
        public PaymentMethodVM PaymentMethodVM { get; set; }
        public PaymentScheduleVM PaymentScheduleVM { get; set; }
    }
}
