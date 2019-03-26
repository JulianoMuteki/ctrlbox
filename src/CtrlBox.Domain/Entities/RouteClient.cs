using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class RouteClient : ValueObject<RouteClient>
    {
        public RouteClient()
        {

        }
        public RouteClient(string clientID, Guid routeID)
        {
            this.ClientID = new Guid(clientID);
            this.RouteID = routeID;
        }

        public Guid RouteID { get; set; }
        public Guid ClientID { get; set; }

        public Route Route { get; set; }
        public Client Client { get; set; }


        protected override bool EqualsCore(RouteClient other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
