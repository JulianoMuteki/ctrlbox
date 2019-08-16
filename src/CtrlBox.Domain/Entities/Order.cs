using CtrlBox.Domain.Common;
using CtrlBox.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class Order : EntityBase
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

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<DeliveryDetail> DeliveriesDetails { get; set; }
        public ICollection<OrderBox> OrdersBoxes { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<BoxProductItem> BoxesProductItems { get; set; }
        public ICollection<OrderProductItem> OrderProductItems { get; set; }

        public Order()
            : base()
        {
            this.OrderProductItems = new HashSet<OrderProductItem>();
            this.BoxesProductItems = new HashSet<BoxProductItem>();
            this.Expenses = new HashSet<Expense>();
            this.DeliveriesDetails = new HashSet<DeliveryDetail>();
            this.OrdersBoxes = new HashSet<OrderBox>();
            this.Sales = new HashSet<Sale>();
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
                this.DtStart = DateTime.Now;
                this.CreatedBy = "Juliano";
                this.FinalizedBy = "Juliano";
            }
        }

        public void FinalizeDelivery()
        {
            this.IsFinalized = true;
            this.DtEnd = DateTime.Now;
            this.DateModified = DateTime.Now;
            this.FinalizedBy = "Juliano";
        }

        public void CreateOrdersBoxes(IEnumerable<Box> boxesToOrder)
        {
            foreach (var box in boxesToOrder)
            {
                OrderBox orderBox = new OrderBox()
                {
                    OrderID = this.Id,
                    BoxID = box.Id
                };
                this.OrdersBoxes.Add(orderBox);
                box.SetFlowOrder();

                if (box.BoxesChildren.Count > 0)
                    CreateOrdersBoxes(box.BoxesChildren);
                else if(box.ProductID != null && box.ProductID != Guid.Empty)
                {
                    CreateOrderProductItem(box);
                }

            }
        }

        private void CreateOrderProductItem(Box box)
        {
            foreach (var boxProductItem in box.BoxesProductItems)
            {
                OrderProductItem orderProductItem = new OrderProductItem()
                {
                    ProductItemID = boxProductItem.ProductItemID,
                    OrderID = this.Id
                };

                this.OrderProductItems.Add(orderProductItem);
                boxProductItem.ProductItem.SetFlowOrder();
            }
        }

        private void PutInTheBoxBoxesProductItemsChildren(Box box)
        {
            if (box.BoxesProductItems.Count > 0)
            {
                //var boxesProductItems = box.BoxesProductItems.Select(x => { x.OrderID = this.Id; return x; }).ToList();

                foreach (BoxProductItem boxProductItem in box.BoxesProductItems)
                {
                    boxProductItem.ProductItem.PutInTheBox();
                    this.BoxesProductItems.Add(boxProductItem);
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
        }
    }
}
