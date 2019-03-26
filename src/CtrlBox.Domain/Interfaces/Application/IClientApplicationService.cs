using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IClientApplicationService : IApplicationServiceBase<Client>
    {

        ICollection<Client> GetAvailable(Guid routeID);
        ICollection<Client> GetNotAvailable(Guid idRoute);
    }
}
