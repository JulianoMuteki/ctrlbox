using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class ProductItem : EntityBase
    {
        public string Barcode { get; set; }
        public FlowStep FlowStep { get; set; }

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
                this.FlowStep = FlowStep.FactoryCreate();
                this.FlowStep.SetAvailable();
            }
        }

        internal void Deliver(bool hasCrossDocking)
        {
            if (!hasCrossDocking)
            {
                this.IsDisable = true;
                this.Status = EProductItemStatus.Sold_Delivered;
                this.FlowStep.SetFlowDelivered();
            }
            else
            {
                this.FlowStep.SetFlowCrossDocking();
            }
        }

        private void ResetLastTrack()
        {
            this.Trackings = this.Trackings.Select(x => { x.IsLastTrack = false; return x; }).ToList();
        }

        public void AddTracking(Guid trackingTypeID, Guid clientID)
        {
            //ResetLastTrack();

            //Tracking tracking = Tracking.FactoryCreate(trackingTypeID, this.Id, null);

            //if (clientID != null && clientID != Guid.Empty)
            //{
            //    tracking.TrackingsClients.Add(TrackingClient.FactoryCreate(tracking.Id, clientID));
            //}

            //this.Trackings.Add(tracking);
        }

        internal void PutInTheBox()
        {
            this.Status = EProductItemStatus.InBox;
            this.FlowStep.SetInBox();
        }

        public void AddInStock(Guid trackingTypeID, Guid clientID)
        {
            this.FlowStep.SetInStock();
            AddTracking(trackingTypeID, clientID);
        }

        public static ProductItem FactoryCreate(Guid productID)
        {
            var productItem = new ProductItem()
            {
                Barcode = $"1{ DateTime.Now.Date.ToString("yyyyMMddHHmmss")}".Substring(0, 14),
                ProductID = productID,
                FlowStep = FlowStep.FactoryCreate()
            };

            productItem.FlowStep.SetAvailable();
            return productItem;
        }
    }
}
