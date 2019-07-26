using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class BoxTrackingClient : ValueObject<BoxTrackingClient>
    {
        public Guid BoxTrackingID { get; set; }
        public BoxTracking BoxTracking { get; set; }

        public Guid ClientID { get; set; }
        public Client Client { get; set; }

        public BoxTrackingClient()
        {

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}