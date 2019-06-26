using CtrlBox.Domain.Common;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class PaymentMethod : EntityBase
    {
        public string MethodName { get; set; }
        public string Descrition { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public PaymentMethod()
            :base()
        {
            this.Payments = new HashSet<Payment>();
        }
    }
}
