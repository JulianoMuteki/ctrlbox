using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class DeliveryDetail : EntityBase
    {
        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        public int QuantityProductItem { get; set; }

        public Guid OrderID { get; set; }
        public Order Order { get; set; }

        public Guid ClientID { get; set; }
        public Client Client { get; set; }
        public ICollection<DeliveryBox> DeliveriesBoxes { get; set; }

        public DeliveryDetail()
            :base()
        {
            this.DeliveriesBoxes = new HashSet<DeliveryBox>();
        }
        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }

        internal void AddDeliveryBox(Guid boxID)
        {
            DeliveryBox deliveryBox = new DeliveryBox()
            {
                BoxID = boxID,
                DeliveryDetailID = this.Id
            };

            this.DeliveriesBoxes.Add(deliveryBox);
        }

        public void MakeDeliveryBox(Box box)
        {
            box.DoDelivery(this, this.QuantityProductItem);
        }
    }
}
