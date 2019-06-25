﻿using CtrlBox.CrossCutting;
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
            try
            {
                var query = _context.Set<Client>()
                                                   .Where(c => !_context.Set<RouteClient>().Where(x => x.RouteID == routeID).Any(r => r.ClientID == c.Id));

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientRepository>("Unexpected error fetching all available clients", nameof(this.GetAvailable), ex);
            }
        }

        public ICollection<Client> GetNotAvailable(Guid routeID)
        {
            try
            {
                var query = _context.Set<Client>().Where(c => _context.Set<RouteClient>().Where(x => x.RouteID == routeID).Any(z => z.ClientID == c.Id));

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientRepository>("Unexpected error fetching all not available clients", nameof(this.GetNotAvailable), ex);
            }
        }
    }
}
