using System;
using System.Collections.Generic;
using CtrlBox.Application.ViewModel;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IDeliveryApplicationService : IApplicationServiceBase<DeliveryVM>
    {
        ICollection<DeliveryVM> GetByUserId(Guid userId);
        void FinalizeDelivery(Guid deliveryID);
    }
}
