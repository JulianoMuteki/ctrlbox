using CtrlBox.Domain.Common;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class PaymentMethod : EntityBase
    {
        public string MethodName { get; set; }
        public string Descrition { get; set; }

        public ICollection<PaymentSchedule> PaymentsSchedules { get; set; }

        public PaymentMethod()
            :base()
        {
            this.PaymentsSchedules = new HashSet<PaymentSchedule>();
        }
    }
}
