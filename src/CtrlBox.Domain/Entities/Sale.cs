using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class Sale : EntityBase
    {
        public Guid ClientID { get; set; }
        public Guid DeliveryID { get; set; }

        public decimal ReceivedValue { get; set; }
        public decimal ForwardValue { get; set; }
        public int TotalReturnedBoxes { get; set; }

    }
}
