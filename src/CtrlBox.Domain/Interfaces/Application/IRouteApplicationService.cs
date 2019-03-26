using CtrlBox.Domain.Entities;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IRouteApplicationService : IApplicationServiceBase<Route>
    {
        ICollection<RouteClient> ConnectRouteToClient(Route route, string clientesIDs);
    }
}
