using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
   public class Picture: EntityBase
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ContentType { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<BoxType> BoxesTypes { get; set; }
        public ICollection<TraceType> TracesTypes { get; set; }

        public Picture()
            :base()
        {
            this.TracesTypes = new HashSet<TraceType>();
            this.Products = new HashSet<Product>();
            this.BoxesTypes = new HashSet<BoxType>();

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
