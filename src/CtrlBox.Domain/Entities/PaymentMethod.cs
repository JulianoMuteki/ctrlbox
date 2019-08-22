using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class PaymentMethod : EntityBase
    {
        public string MethodName { get; set; }
        public string Descrition { get; set; }

        public ICollection<PaymentSchedule> PaymentsSchedules { get; set; }

        private PaymentMethod()
            : base()
        {
            this.PaymentsSchedules = new HashSet<PaymentSchedule>();
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
