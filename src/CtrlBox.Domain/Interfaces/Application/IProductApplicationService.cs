using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IProductApplicationService : IApplicationServiceBase<ProductVM>
    {
        ICollection<ClientProductValueVM> ConnectRouteToClient(ICollection<ClientProductValueVM> clientsProductsVMs);

        ICollection<DeliveryDetailVM> GetDeliveryProducts(Guid deliveryID);
        ICollection<ClientProductValueVM> GetClientsProductsByClientID(Guid clientID);

        void GenerateProductItem(Guid productID, int quantity);
        ICollection<ProductItemVM> GetProductsItems();
        ICollection<ProductItemVM> GetProductsItemsAvailable(int quantity);
        int GetTotalProductItemByProductID(Guid productID);
        void AddStockProduct(Guid productID, Guid clientID, Guid trackingTypeID, int quantity);
        ICollection<ProductItemVM> GetAvailableStockProductItemsByClientIDAndProductID(Guid productID, Guid clientID);
        void AddBoxStockWithProductItems(Guid boxTypeID, Guid trackingTypeID, Guid clientID, Guid productID, int quantity);
        ICollection<BoxVM> GetAvailableBoxesByBoxTypeIDAndProductID(Guid boxTypeID, Guid clientID);
        void AddBoxStockWithBoxes(Guid boxTypeID, Guid trackingTypeID, Guid clientID, Guid boxTypeChildID, int quantity);
        void AddStock(StockVM stockVM);
        object GetStocks();
    }
}
