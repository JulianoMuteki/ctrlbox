using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class BoxVM: ViewModelBase
    {
        public string Barcode { get; set; }
        public string Description { get; set; }

        public Guid BoxTypeID { get; set; }
        public BoxTypeVM BoxType { get; set; }

        public Guid? BoxParentID { get; set; }
        public BoxVM BoxParent { get; set; }

        public Guid? ProductID { get; set; }
        public ProductVM Product { get; set; }

        public ICollection<BoxVM> ChildrenBoxes { get; set; }
        public ICollection<BoxProductItemVM> BoxesProductItems { get; set; }

        public BoxVM()
            : base()
        {
            this.ChildrenBoxes = new HashSet<BoxVM>();
            this.BoxesProductItems = new HashSet<BoxProductItemVM>();
        }
    }
}
