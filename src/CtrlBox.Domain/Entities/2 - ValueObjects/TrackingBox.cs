using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class TrackingBox : ValueObject<TrackingBox>
    {
        public Guid TrackingID { get; set; }
        public Tracking Tracking { get; set; }

        public Guid BoxID { get; set; }
        public Box Box { get; set; }

        private TrackingBox()
        {

        }

        public static TrackingBox FactoryCreate(Guid trackingID, Guid boxID)
        {
            return new TrackingBox() { BoxID = boxID, TrackingID = trackingID };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}