using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Repository
{
    public interface IDeliveryRepository : IGenericRepository<Delivery>
    {
        ICollection<DeliveryProduct> GetDeliveryProductsLoad(Guid deliveryID);
        ICollection<Delivery> GetDeliveryRouteLoad();
    }
}
