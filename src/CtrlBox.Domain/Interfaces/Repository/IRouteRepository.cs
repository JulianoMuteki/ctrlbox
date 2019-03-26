using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Interfaces.Repository
{
    public interface IRouteRepository : IGenericRepository<Route>
    {
        ICollection<Route> GetCustom();
    }
}
