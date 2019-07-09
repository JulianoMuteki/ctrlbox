using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class RouteClient : ValueObject<RouteClient>
    {
        public Guid RouteID { get; set; }
        public Guid ClientID { get; set; }

        public Route Route { get; set; }
        public Client Client { get; set; }

        public RouteClient()
        {

        }
        public RouteClient(string clientID, Guid routeID)
        {
            this.ClientID = new Guid(clientID);
            this.RouteID = routeID;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}
