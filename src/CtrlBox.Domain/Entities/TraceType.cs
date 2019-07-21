using CtrlBox.Domain.Common;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class TraceType : EntityBase
    {
        public TypeTrace TypeTrace { get; set; }
        public string Desciption { get; set; }

        public ICollection<Traceability> Traceabilities { get; set; }

        public TraceType()
            : base()
        {
            this.Traceabilities = new HashSet<Traceability>();
        }
    }

    public enum TypeTrace
    {
        Place,
        State
    }
}
