using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class RouteClient : ValueObject<RouteClient>
    {
        public Guid RouteID { get; set; }
        public Guid ClientID { get; set; }


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
