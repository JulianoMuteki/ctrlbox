using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Infra.Repository.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(CtrlBoxContext context)
            : base(context)
        {

        }

        public ICollection<Client> GetAvailable(Guid routeID)
        {
            var clientsAvailable = _context.Set<Client>()
                                               .Where(x => !_context.Set<RouteClient>().Any(r=> r.ClientID == x.Id))                                           
                                               .ToList();

            return clientsAvailable;
        }

        public ICollection<Client> GetNotAvailable(Guid routeID)
        {
            var clientsNotAvailable = _context.Set<Client>()
                                               .Where(x => _context.Set<RouteClient>().Any(r => r.ClientID == x.Id))
                                               .ToList();

            return clientsNotAvailable;
        }
    }
}
