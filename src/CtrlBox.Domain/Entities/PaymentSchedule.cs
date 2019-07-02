using CtrlBox.Domain.Common;
using System;

namespace CtrlBox.Domain.Entities
{
    public class PaymentSchedule: EntityBase
    {
        public decimal BenefitValue { get; set; }
        public DateTime ExprireDate { get; set; }
        public DateTime? RealizedDate { get; set; }

        public Guid PaymentID { get; set; }
        public Payment Payment { get; set; }

        public Guid PaymentMethodID { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public PaymentSchedule()
            :base()
        {
           
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }
    }
}
