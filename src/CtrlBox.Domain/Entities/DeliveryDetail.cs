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

        private DeliveryDetail()
            : base()
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
            if (!this.DeliveriesBoxes.Any(x => x.BoxID == boxID))
            {
                DeliveryBox deliveryBox = DeliveryBox.FactoryCreate(boxID, this.Id);
                this.DeliveriesBoxes.Add(deliveryBox);
            }
        }

        public void MakeDeliveryBox(Box box)
        {
            box.DoDelivery(this, this.QuantityProductItem);
        }

        public static DeliveryDetail FactoryCreate(Guid clientID, Guid productID, Guid orderID, int quantity)
        {
            return new DeliveryDetail()
            {
                ClientID = clientID,
                ProductID = productID,
                OrderID = orderID,
                QuantityProductItem = quantity
            };
        }

        public List<Box> Create(List<Box> boxesProductsAvailable, Guid trackingTypeID)
        {
            var totalProductItemsDelivery = this.QuantityProductItem;

            List<Box> boxesProductsUpdate = new List<Box>();
            foreach (var box in boxesProductsAvailable)
            {
                if (totalProductItemsDelivery == 0)
                    break;

                var totalDelivered = box.DoDelivery(this, totalProductItemsDelivery);
                box.AddTracking(trackingTypeID, this.ClientID);
                box.AddTrackingProductItems(trackingTypeID, this.ClientID);

                totalProductItemsDelivery -= totalDelivered;
                boxesProductsUpdate.Add(box);
            }

            return boxesProductsUpdate;
        }
    }
}
