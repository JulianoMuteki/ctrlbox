using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class PaymentSchedule: EntityBase
    {
        public decimal BenefitValue { get; set; }
        public DateTime ExprireDate { get; set; }
        public DateTime? RealizedDate { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public PaymentSchedule()
            :base()
        {
            this.Payments = new HashSet<Payment>();
        }
    }
}
