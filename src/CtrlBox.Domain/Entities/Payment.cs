using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
  public  class Payment : EntityBase
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public int NumberParcels { get; set; }
        public decimal RemainingValue { get; set; }

        public Guid SaleID { get; set; }
        public Sale Sale { get; set; }

        public ICollection<PaymentSchedule> PaymentsSchedules { get; set; }

        public Payment()
            :base()
        {
            this.PaymentsSchedules = new HashSet<PaymentSchedule>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
                this.IsPaid = false;

                this.PaymentsSchedules = this.PaymentsSchedules.Select(x => { x.PaymentID = this.Id; return x; }).ToList();
            }
        }
    }
}
