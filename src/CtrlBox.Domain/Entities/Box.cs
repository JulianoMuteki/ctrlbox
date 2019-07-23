using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class Box : EntityBase
    {
        public string Description { get; set; }
        public int StatusBox { get; set; }

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

        public void LoadProductItems(IEnumerable<ProductItem> productItems)
        {
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
        }

        public List<Box> AddChildren(List<Box> boxesChildren)
        {
            return boxesChildren.Select(x => { x.BoxParentID = this.Id; return x; }).ToList();        
        }

    }
}
