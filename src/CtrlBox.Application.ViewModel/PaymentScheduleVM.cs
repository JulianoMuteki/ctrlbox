using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class PaymentScheduleVM : ViewModelBase
    {
        public decimal BenefitValue { get; set; }
        public string ExprireDate { get; set; }

        public DateTime ExprireDateFormat { get
            {
                return DateTime.Parse(ExprireDate, null, System.Globalization.DateTimeStyles.RoundtripKind);
            }
                
       }

        public DateTime? RealizedDate { get; set; }
        public Guid PaymentMethodID { get; set; }
    }
}
