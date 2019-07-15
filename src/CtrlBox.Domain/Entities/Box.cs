using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Box : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsProductBox { get; set; }

        public Guid? BoxParentID { get; set; }
        public Box BoxParent { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Box> ChildrenBoxes { get; set; }

        public Box()
            : base()
        {
            this.Products = new HashSet<Product>();
            this.ChildrenBoxes = new HashSet<Box>();
        }
    }
}
