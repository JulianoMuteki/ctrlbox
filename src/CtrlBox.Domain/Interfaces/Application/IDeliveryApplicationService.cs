using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IDeliveryApplicationService : IApplicationServiceBase<Delivery>
    {
        Delivery Add(Delivery entity);
    }
}
