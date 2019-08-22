using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class ProductItem : EntityBase
    {
        public string Barcode { get; set; }
        public EFlowStep EFlowStep { get; set; }

        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public EProductItemStatus Status { get; set; }

        public ICollection<BoxProductItem> BoxesProductItems { get; set; }
        public ICollection<Tracking> Trackings { get; set; }
        public ICollection<OrderProductItem> OrderProductItems { get; set; }

        private ProductItem()
            : base()
        {
            this.OrderProductItems = new HashSet<OrderProductItem>();
            this.Trackings = new HashSet<Tracking>();
            this.BoxesProductItems = new HashSet<BoxProductItem>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
                this.Status = EProductItemStatus.AvailableStock;
                this.EFlowStep = EFlowStep.Available;
            }
        }

        internal void Deliver()
        {
            this.IsDisable = true;
            this.Status = EProductItemStatus.Sold_Delivered;
            SetFlowDelivered();
        }

        internal void FinishDelivery(bool hasCrossDocking)
        {
            if (hasCrossDocking)
                this.EFlowStep = EFlowStep.InStock;
            else
                this.EFlowStep = EFlowStep.Delivery;
        }

        public void AddTracking(Guid trackingTypeID, Guid clientID)
        {
            Tracking tracking = Tracking.FactoryCreate(trackingTypeID, this.Id, null);

            if (clientID != null && clientID != Guid.Empty)
            {
                tracking.TrackingsClients.Add(TrackingClient.FactoryCreate(tracking.Id, clientID));
            }

            this.Trackings.Add(tracking);
        }

        private void SetFlowDelivered()
        {
            this.EFlowStep = EFlowStep.Delivery;
        }

        internal void PutInTheBox()
        {
            this.Status = EProductItemStatus.InBox;
            this.EFlowStep = EFlowStep.InBox;
        }

        internal void SetFlowOrder()
        {
            this.EFlowStep = EFlowStep.Order;
        }

        public void AddInStock(Guid trackingTypeID, Guid clientID)
        {
            this.EFlowStep = EFlowStep.InStock;
            AddTracking(trackingTypeID, clientID);
        }

        public static ProductItem FactoryCreate(Guid productID)
        {
            return new ProductItem()
            {
                Barcode = $"1{ DateTime.Now.Date.ToString("yyyyMMddHHmmss")}".Substring(0, 14),
                ProductID = productID,
                EFlowStep = EFlowStep.Available
            };
        }
    }
}
