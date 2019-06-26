using CtrlBox.Domain.Common;
using System;

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
        public Guid PaymentMethodID { get; set; }
        public Guid? PaymentScheduleID { get; set; }

        public Sale Sale { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentSchedule PaymentSchedule { get; set; }

        public Payment()
            :base()
        {

        }
    }
}
