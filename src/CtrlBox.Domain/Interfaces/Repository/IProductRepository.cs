using System;
using System.Collections.Generic;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;

namespace CtrlBox.Domain.Interfaces.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        int GetTotalProductItemByProductID(Guid productID);
        ICollection<ProductItem> GetAvailableProductItemByProductID(Guid productID, int quantity);
        ICollection<ProductItem> GetAvailableStockProductItemsByClientIDAndProductID(Guid productID, Guid clientID);
        ICollection<Box> GetBoxesInStockByBoxTypeIDAndClientID(Guid boxTypeID, Guid clientID);
    }
}
