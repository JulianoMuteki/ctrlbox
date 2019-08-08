using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class ProductItem: EntityBase
    {
        public string Barcode { get; set; }
        public EFlowStep EFlowStep { get; set; }

        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public EProductItemStatus Status { get; set; }

        public ICollection<BoxProductItem> BoxesProductItems { get; set; }
        public ICollection<Tracking> Trackings { get; set; }

        public ProductItem()
            :base()
        {
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
            Tracking tracking = new Tracking()
            {
                TrackingTypeID = trackingTypeID,
                ProductItemID = this.Id
            };

            if (clientID != null && clientID != Guid.Empty)
            {
                tracking.TrackingsClients.Add(new TrackingClient()
                {
                    ClientID = clientID,
                    TrackingID = tracking.Id
                });
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
    }
}
