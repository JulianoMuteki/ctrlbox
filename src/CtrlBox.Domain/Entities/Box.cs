using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class Box : EntityBase
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int StatusBox { get; set; }

        public Guid BoxTypeID { get; set; }
        public BoxType BoxType { get; set; }

        public Guid? BoxParentID { get; set; }
        public Box BoxParent { get; set; }

        public Guid? ProductID { get; set; }
        public Product Product { get; set; }

        public ICollection<Box> BoxesChildren { get; set; }
        public ICollection<BoxProductItem> BoxesProductItems { get; set; }
        public ICollection<DeliveryBox> DeliveriesBoxes { get; set; }

        public Box()
            : base()
        {
            this.BoxesChildren = new HashSet<Box>();
            this.BoxesProductItems = new HashSet<BoxProductItem>();
            this.DeliveriesBoxes = new HashSet<DeliveryBox>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }

        public void LoadProductItems(IEnumerable<ProductItem> productItems)
        {
            foreach (var item in productItems)
            {
                BoxProductItem boxProductItem = new BoxProductItem();
                boxProductItem.BoxID = this.Id;
                boxProductItem.ProductItemID = item.Id;
                item.PutInTheBox();

                this.BoxesProductItems.Add(boxProductItem);
            }
        }

        public List<Box> AddChildren(List<Box> boxesChildren)
        {
            return boxesChildren.Select(x => { x.BoxParentID = this.Id; return x; }).ToList();        
        }
    }
}
