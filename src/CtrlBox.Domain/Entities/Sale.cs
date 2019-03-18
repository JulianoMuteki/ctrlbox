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

        public virtual Client   Client { get; set; }
        public virtual Delivery Delivery { get; set; }

        public ICollection<Check> Checks { get; set; }
        public ICollection<DeliveryProduct> DeliveriesProducts { get; set; }

        public Sale()
        {
            this.Checks = new HashSet<Check>();
            this.DeliveriesProducts = new HashSet<DeliveryProduct>();
        }
    }
}
