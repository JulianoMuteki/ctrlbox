using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
   public class Delivery: EntityBase
    {
        public Guid RouteID { get; set; }

        public bool IsFinalized { get; set; }
        public DateTime DtStart { get; set; }
        public DateTime DtEnd { get; set; }
        public string CreatedBy { get; set; }
        public string FinalizedBy { get; set; }
    }
}
