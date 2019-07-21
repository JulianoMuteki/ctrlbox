using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class TraceType : EntityBase
    {
        public TypeTrace TypeTrace { get; set; }
        public string Description { get; set; }

        public ICollection<Traceability> Traceabilities { get; set; }

        public TraceType()
            : base()
        {
            this.Traceabilities = new HashSet<Traceability>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }
    }

    public enum TypeTrace
    {
        Place = 1,
        State = 2
    }
}
