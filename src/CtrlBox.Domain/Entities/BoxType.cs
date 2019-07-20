using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class BoxType : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        // public bool HasProducts { get; set; }

        public Guid? PictureID { get; set; }
        public Picture Picture { get; set; }

        public ICollection<Box> Boxes { get; set; }

        public BoxType()
        : base()
        {
            this.Boxes = new HashSet<Box>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }
    }
}
