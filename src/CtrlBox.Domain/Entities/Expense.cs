using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class Expense : EntityBase
    {
        public Guid DeliveryID { get; set; }

        public double Value { get; set; }

        public string Description { get; set; }

        public virtual Delivery Delivery { get; set; }


    }
}
