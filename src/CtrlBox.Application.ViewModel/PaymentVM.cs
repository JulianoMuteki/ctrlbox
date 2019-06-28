using System;
using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class PaymentVM : ViewModelBase
    {
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public int NumberParcels { get; set; }
        public decimal RemainingValue { get; set; }

        public ICollection<PaymentScheduleVM> PaymentsSchedules { get; set; }

        public PaymentVM()
        {
            this.PaymentsSchedules = new HashSet<PaymentScheduleVM>();
        }
    }
}
