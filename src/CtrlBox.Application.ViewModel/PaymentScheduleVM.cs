using System;

namespace CtrlBox.Application.ViewModel
{
    public class PaymentScheduleVM : ViewModelBase
    {
        public decimal BenefitValue { get; set; }
        public string ExprireDate { get; set; }
        public DateTime? RealizedDate { get; set; }

        public DateTime ExprireDateFormat { get
            {
                return DateTime.Parse(ExprireDate, null, System.Globalization.DateTimeStyles.RoundtripKind);
            }
                
       }
      
        public Guid PaymentID { get; set; }
        public PaymentVM Payment { get; set; }

        public Guid PaymentMethodID { get; set; }
        public PaymentMethodVM PaymentMethod { get; set; }
    }
}
