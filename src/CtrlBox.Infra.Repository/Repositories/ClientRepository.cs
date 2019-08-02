using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;
using Microsoft.EntityFrameworkCore;
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

        public Client GetByIDWithOptionsTypes(Guid id)
        {
            try
            {
                var query = _context.Set<Client>()
                     .Include(x => x.ClientsOptionsTypes).ThenInclude(c => c.OptiontType)
                     .Where(x => x.Id == id);
                    
                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientRepository>("Unexpected error fetching GetByIDWithCategories", nameof(this.GetByIDWithOptionsTypes), ex);
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

        public ICollection<Client> GetByRouteID(Guid routeID)
        {
            try
            {
                var query = _context.Set<Client>()

                           .Join(_context.Set<RouteClient>(),
                              cl => cl.Id,
                              rt => rt.ClientID,
                              (cl, rt) => new { Client = cl, rt.RouteID })

                           .Where(x => x.RouteID == routeID)
                           .Select(x => x.Client)
                              .Include(c => c.Sales)
                              .Include(c => c.DeliveriesDetails);



                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetByRouteID", nameof(this.GetByRouteID), ex);
            }
        }
    }
}
