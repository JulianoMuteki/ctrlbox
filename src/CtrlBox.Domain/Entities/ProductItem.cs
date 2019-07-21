using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class ProductItem: EntityBase
    {
        public string Barcode { get; set; }
        public string Weight { get; set; }

        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public bool InBox { get; set; }

        public ICollection<BoxProductItem> LoadBoxesProductItems { get; set; }
        public ICollection<Traceability> Traceabilities { get; set; }

        public ProductItem()
            :base()
        {
            this.Traceabilities = new HashSet<Traceability>();
            this.LoadBoxesProductItems = new HashSet<BoxProductItem>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
                this.InBox = false;
            }
        }

        internal void PutInTheBox()
        {
            this.InBox = true;
        }
    }
}
