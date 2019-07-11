using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface ISaleApplicationService : IApplicationServiceBase<SaleVM>
    {
        ICollection<SaleVM> FindAllByDelivery(Guid deliveryID);
        SaleVM GetByClientIDAndDeliveryID(Guid clientID, Guid deliveryID);
        SaleVM GetInvoiceSaleByID(Guid saleID);
    }
}
