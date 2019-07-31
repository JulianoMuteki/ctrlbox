using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IProductApplicationService : IApplicationServiceBase<ProductVM>
    {
        ICollection<ClientProductValueVM> ConnectRouteToClient(ICollection<ClientProductValueVM> clientsProductsVMs);

        ICollection<DeliveryProductVM> GetDeliveryProducts(Guid deliveryID);
        ICollection<ClientProductValueVM> GetClientsProductsByClientID(Guid clientID);

        void GenerateProductItem(Guid productID, int quantity);
        ICollection<ProductItemVM> GetProductsItems();
        ICollection<ProductItemVM> GetProductsItemsAvailable(int quantity);
    }
}
