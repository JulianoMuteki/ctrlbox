using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class ProductItem: EntityBase
    {
        public string Barcode { get; set; }

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
            }
        }

        internal void Deliver()
        {
            this.IsDisable = true;
            this.Status = EProductItemStatus.Sold_Delivered;
        }

        internal void PutInTheBox()
        {
            this.Status = EProductItemStatus.InBox;
        }
    }
}
