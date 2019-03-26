using System.Collections.Generic;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;

namespace CtrlBox.Infra.Repository.Repositories
{
    public class RouteRepository : GenericRepository<Route>, IRouteRepository
    {
        public RouteRepository(CtrlBoxContext context)
            : base(context)
        {
           
        }

        public ICollection<Route> GetCustom()
        {
            throw new System.NotImplementedException();
        }
    }
}