using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Repository
{
    public interface IDeliveryRepository : IGenericRepository<Order>
    {
        ICollection<DeliveryDetail> GetDeliveryProductsLoad(Guid deliveryID);
        ICollection<Order> GetDeliveryRouteLoad();
        ICollection<Order> GetDeliveryByUserWithRoute(Guid userId);

        Order GetResumeDeliveryById(Guid deliveryID);
    }
}
