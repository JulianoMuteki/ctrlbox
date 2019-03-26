using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Repository
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        ICollection<Client> GetAvailable(Guid routeID);
        ICollection<Client> GetNotAvailable(Guid routeID);
    }
}
