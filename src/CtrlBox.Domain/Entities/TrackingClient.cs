using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class TrackingClient : ValueObject<TrackingClient>
    {
        public Guid TrackingID { get; set; }
        public Tracking Tracking { get; set; }

        public Guid ClientID { get; set; }
        public Client Client { get; set; }

        public TrackingClient()
        {

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}