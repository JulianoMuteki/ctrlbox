using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Box : EntityBase
    {
        public string Barcode { get; set; }
        public string Description { get; set; }

        public Guid BoxTypeID { get; set; }
        public BoxType BoxType { get; set; }

        public Guid? BoxParentID { get; set; }
        public Box BoxParent { get; set; }

        public Guid? ProductID { get; set; }
        public Product Product { get; set; }

        public ICollection<Box> ChildrenBoxes { get; set; }
        public ICollection<BoxProductItem> BoxesProductItems { get; set; }

        public Box()
            : base()
        {
            this.ChildrenBoxes = new HashSet<Box>();
            this.BoxesProductItems = new HashSet<BoxProductItem>();
        }
    }
}
