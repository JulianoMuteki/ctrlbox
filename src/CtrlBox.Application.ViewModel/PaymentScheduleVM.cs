using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class PaymentScheduleVM : ViewModelBase
    {
        public decimal BenefitValue { get; set; }
        public DateTime ExprireDate { get; set; }
        public DateTime? RealizedDate { get; set; }

        public ICollection<PaymentVM> PaymentsVMs { get; set; }
    }
}
