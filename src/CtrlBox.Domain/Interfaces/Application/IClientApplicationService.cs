using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IClientApplicationService : IApplicationServiceBase<ClientVM>
    {
        ICollection<ClientVM> GetAvailable(Guid routeID);
        ICollection<ClientVM> GetNotAvailable(Guid idRoute);
        ICollection<ClientVM> GetByRouteID(Guid routeID);
        ICollection<OptiontTypeVM> GetAllOptionsTypes();
        void AddOptionType(OptiontTypeVM optiontTypeVM);
    }
}
