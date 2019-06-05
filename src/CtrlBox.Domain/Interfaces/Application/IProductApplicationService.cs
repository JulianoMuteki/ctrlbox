﻿using CtrlBox.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IProductApplicationService : IApplicationServiceBase<Product>
    {
        ICollection<ClientProductValue> ConnectRouteToClient(ICollection<ClientProductValue> clientsProducts);
        int AddProductStock(ICollection<StockProduct> stocksProducts);
        ICollection<StockProduct> GetProductsStock();

        ICollection<DeliveryProduct> GetDeliveryProducts(Guid deliveryID);
        ICollection<ClientProductValue> GetClientsProductsByClientID(Guid clientID);
        Stock GetStock();

        void AddStock(int stockTotal);
    }
}
