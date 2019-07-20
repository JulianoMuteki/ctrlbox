using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IProductApplicationService : IApplicationServiceBase<ProductVM>
    {
        ICollection<ClientProductValueVM> ConnectRouteToClient(ICollection<ClientProductValueVM> clientsProductsVMs);
        int AddProductStock(ICollection<StockProductVM> stocksProductsVMs);
        ICollection<StockProductVM> GetProductsStock();

        ICollection<DeliveryProductVM> GetDeliveryProducts(Guid deliveryID);
        ICollection<ClientProductValueVM> GetClientsProductsByClientID(Guid clientID);
        StockVM GetStock();

        void AddStock(int stockTotal);
        void GenerateProductItem(Guid productID, int quantity);
        ICollection<ProductItemVM> GetProductsItems();
    }
}
