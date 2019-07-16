using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class LoadBox: EntityBase
    {
        public string Barcode { get; set; }
        public string Description { get; set; }

        public Guid BoxID { get; set; }
        public Box Box { get; set; }

        //public Guid? LoadBoxParentID { get; set; }
        //public LoadBox LoadBoxParent { get; set; }

        //public Guid? ProductID { get; set; }
        //public Product Product { get; set; }

        //public ICollection<LoadBox> ChildrenLoadBoxes { get; set; }
        public ICollection<LoadBoxProductItem> LoadBoxesProductItems { get; set; }

        public LoadBox()
            :base()
        {
           // this.ChildrenLoadBoxes = new HashSet<LoadBox>();
            this.LoadBoxesProductItems = new HashSet<LoadBoxProductItem>();
        }
    }
}
