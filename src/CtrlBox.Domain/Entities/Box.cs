using CtrlBox.Domain.Common;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Box : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsProductBox { get; set; }

        public ICollection<LoadBox> LoadBoxes { get; set; }

        public Box()
        : base()
        {
            this.LoadBoxes = new HashSet<LoadBox>();
        }
    }
}
