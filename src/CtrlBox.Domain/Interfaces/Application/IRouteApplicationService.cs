using CtrlBox.Application.ViewModel;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IRouteApplicationService : IApplicationServiceBase<RouteVM>
    {
        ICollection<RouteClientVM> ConnectRouteToClient(ICollection<RouteClientVM> routesClientsVM);
        ICollection<RouteClientVM> RemoveRouteFromClient(ICollection<RouteClientVM> routesClientsVM);
        ICollection<RouteVM> GetRoutesWithoutOpenOrder();
    }
}
