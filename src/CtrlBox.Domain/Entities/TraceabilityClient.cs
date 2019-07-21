using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class TraceabilityClient : ValueObject<TraceabilityClient>
    {
        public Guid TraceID { get; set; }
        public Traceability Traceability { get; set; }

        public Guid ClientID { get; set; }
        public Client Client { get; set; }

        public TraceabilityClient()
        {

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}