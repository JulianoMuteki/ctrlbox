using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface ISaleApplicationService : IApplicationServiceBase<Sale>
    {
        ICollection<Sale> FindAllByDelivery(Guid deliveryID);
    }
}
