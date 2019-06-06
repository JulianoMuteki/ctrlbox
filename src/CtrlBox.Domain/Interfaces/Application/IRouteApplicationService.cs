using CtrlBox.Application.ViewModel;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IRouteApplicationService : IApplicationServiceBase<RouteVM>
    {
        ICollection<RouteClientVM> ConnectRouteToClient(ICollection<RouteClientVM> routesClientsVM);
    }
}
