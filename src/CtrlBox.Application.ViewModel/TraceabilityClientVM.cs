using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class TraceabilityClientVM
    {
        public Guid TraceID { get; set; }
        public TraceabilityVM Traceability { get; set; }

        public Guid ClientID { get; set; }
        public ClientVM Client { get; set; }
    }
}
