using CtrlBox.Domain.Common;
using CtrlBox.Domain.Identity;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Shipment : EntityBase
    {
        public Guid RouteID { get; set; }
        public Guid UserID { get; set; }

        public bool IsFinalized { get; set; }
        public DateTime DtStart { get; set; }
        public DateTime? DtEnd { get; set; }
        public string CreatedBy { get; set; }
        public string FinalizedBy { get; set; }

        public Route Route { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<ShipmentBox> ShipmentBoxes { get; set; }

        private Shipment()
            : base()
        {
            this.ShipmentBoxes = new HashSet<ShipmentBox>();
        }

        /// <summary>
        /// Inicialize property and ID - GUID
        /// </summary>
        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
                this.IsFinalized = false;

            }
        }

        public void FinalizeDelivery()
        {
            this.IsFinalized = true;
            this.DtEnd = DateTime.Now;
            this.DateModified = DateTime.Now;
            this.FinalizedBy = "Juliano";
        }

        public void CreateShipmentBoxes(IEnumerable<Box> boxesToOrder)
        {
            foreach (var box in boxesToOrder)
            {
                ShipmentBox orderBox = ShipmentBox.FactoryCreate(this.Id, box.Id);

                this.ShipmentBoxes.Add(orderBox);
                box.FlowStep.SetFlowOrder();

                if (box.BoxesChildren.Count > 0)
                    CreateShipmentBoxes(box.BoxesChildren);
                else if (box.ProductID != null && box.ProductID != Guid.Empty)
                {
                  //  CreateOrderProductItem(box);
                }
            }
        }

        //private void CreateOrderProductItem(Box box)
        //{
        //    foreach (var boxProductItem in box.BoxesProductItems)
        //    {
        //        OrderProductItem orderProductItem = OrderProductItem.FactoryCreate(boxProductItem.ProductItemID, this.Id);
        //        this.OrderProductItems.Add(orderProductItem);
        //        boxProductItem.ProductItem.FlowStep.SetFlowOrder();
        //    }
        //}

        private void PutInTheBoxBoxesProductItemsChildren(Box box)
        {
            if (box.BoxesProductItems.Count > 0)
            {
                foreach (BoxProductItem boxProductItem in box.BoxesProductItems)
                {
                    boxProductItem.ProductItem.PutInTheBox();
                   // this.BoxesProductItems.Add(boxProductItem);
                }
            }
            else
            {
                foreach (var boxChild in box.BoxesChildren)
                {
                    PutInTheBoxBoxesProductItemsChildren(boxChild);
                }
            }
        }

        public void Close()
        {
            this.IsFinalized = true;
            this.DtEnd = DateTime.Now;
            this.FinalizedBy = "Juliano";
            this.Route.OrderClosed();
        }
    }
}
