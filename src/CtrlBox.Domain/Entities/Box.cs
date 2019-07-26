using CtrlBox.CrossCutting;
using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class Box : EntityBase
    {
        public string Description { get; set; }
        public BoxStatus Status { get; set; }

        public int PorcentFull { get; set; }

        public Guid BoxTypeID { get; set; }
        public BoxType BoxType { get; set; }

        public Guid? BoxParentID { get; set; }
        public Box BoxParent { get; set; }

        public Guid? ProductID { get; set; }
        public Product Product { get; set; }

        public BoxBarcode BoxBarcode { get; set; }

        public ICollection<Box> BoxesChildren { get; set; }
        public ICollection<BoxProductItem> BoxesProductItems { get; set; }
        public ICollection<DeliveryBox> DeliveriesBoxes { get; set; }
        public ICollection<Traceability> Traceabilities { get; set; }

        public Box()
            : base()
        {
            this.Traceabilities = new HashSet<Traceability>();
            this.BoxesChildren = new HashSet<Box>();
            this.BoxesProductItems = new HashSet<BoxProductItem>();
            this.DeliveriesBoxes = new HashSet<DeliveryBox>();
        }

        public void Destructor()
        {
            this.BoxType = null;
            this.Traceabilities = new HashSet<Traceability>();
            this.BoxesChildren = new HashSet<Box>();
            this.BoxesProductItems = new HashSet<BoxProductItem>();
            this.DeliveriesBoxes = new HashSet<DeliveryBox>();
        }

        public void SetBoxType(BoxType boxType)
        {
            this.BoxType = boxType;
        }

        public void SetBarcode(int i)
        {
            this.BoxBarcode = new BoxBarcode();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
                this.BoxBarcode = new BoxBarcode();
            }
        }

        public void LoadProductItems(ICollection<ProductItem> productItems)
        {
            if (productItems.Count > this.BoxType.MaxProductsItems)
                throw CustomException.Create<Box>("Unexpected error MaxProductsItems ", nameof(this.LoadProductItems));

            foreach (var item in productItems)
            {
                BoxProductItem boxProductItem = new BoxProductItem
                {
                    BoxID = this.Id,
                    ProductItemID = item.Id
                };
                item.PutInTheBox();

                this.BoxesProductItems.Add(boxProductItem);
            }

            LoadFullBoxCompletedProductItems();
        }

        public int CountQuantityProductItems { get; set; }

        public void TakeOutOfTheBoxProductItems(Guid productID, int quantity)
        {
            if (this.ProductID != Guid.Empty && this.BoxesProductItems.Count > 0)
            {
                var boxProductsItems = this.BoxesProductItems.Where(x => x.ProductItem.ProductID == productID && x.IsDelivered == false).Take(quantity).ToList();

                foreach (var boxProductItem in boxProductsItems)
                {
                    boxProductItem.Deliver();
                }

                this.BoxParent.CountQuantityProductItems -= boxProductsItems.Count;
                LoadFullBoxCompletedProductItems();
            }
            else
            {
                this.CountQuantityProductItems = quantity;

                foreach (var boxChild in this.BoxesChildren)
                {
                    if (this.CountQuantityProductItems == 0)
                        break;

                    boxChild.TakeOutOfTheBoxProductItems(productID, this.CountQuantityProductItems);
                }

                if (this.BoxParentID != null && this.BoxParentID != Guid.Empty)
                    this.BoxParent.CountQuantityProductItems = this.CountQuantityProductItems;

                LoadFullBoxCompletedChildrem();
            }
        }

        private void LoadFullBoxCompletedProductItems()
        {
            this.PorcentFull = (int)Math.Round((double)(100 * this.BoxesProductItems.Where(x => x.IsDelivered == false).ToList().Count) / this.BoxType.MaxProductsItems);
        }

        private void LoadFullBoxCompletedChildrem()
        {
            this.PorcentFull = (int)Math.Round((double)(100 * this.BoxesChildren.Count) / this.BoxType.MaxProductsItems);
            this.DateModified = DateTime.Now;
        }

        public List<Box> AddChildren(List<Box> boxesChildren)
        {
            this.BoxesChildren = boxesChildren.Select(x => { x.BoxParentID = this.Id; return x; }).ToList();
            LoadFullBoxCompletedChildrem();

            return this.BoxesChildren.ToList();
        }

    }
}
