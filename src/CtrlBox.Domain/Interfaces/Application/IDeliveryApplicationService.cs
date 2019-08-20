using System;
using System.Collections.Generic;
using CtrlBox.Application.ViewModel;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IDeliveryApplicationService : IApplicationServiceBase<OrderVM>
    {
        ICollection<OrderVM> GetByUserId(Guid userId);
        void FinalizeDelivery(Guid deliveryID);
        OrderVM GetResumeDeliveryById(Guid deliveryID);
        void MakeDelivery(OrderVM deliveryVM, Guid trackingTypeID);
        void FinishDelivery(Guid orderID, bool hasCrossDocking);
    }
}
