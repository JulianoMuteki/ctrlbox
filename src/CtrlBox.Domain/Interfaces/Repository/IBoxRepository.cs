﻿using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Repository
{
    public interface IBoxRepository : IGenericRepository<Box>
    {
        ICollection<Box> GetAllWithBoxTypeAndProduct();
        ICollection<Box> GetBoxesParentsWithBoxType();
        ICollection<Box> GetBoxesByDeliveryWithBoxType(Guid deliveryID);
        ICollection<Box> GetBoxesByBoxTypeIDWithProductItems(Guid boxTypeID, int quantity);
        ICollection<ProductItem> GetOrderProductItemByDeliveryID(Guid deliveryID);
        ICollection<Box> GetBoxesByBoxWithChildren(Guid boxID);
        Box GetBoxesByIDWithBoxTypeAndProductItems(Guid boxID);
        ICollection<Box> GetBoxesParentsWithBoxTypeEndProduct();
        ICollection<Box> GetBoxesByDeliveryIDWithProductItems(Guid deliveryID);
        ICollection<Box> GetBoxesParentsByOrderIDWithProductItems(Guid orderID);
        ICollection<Box> GetBoxesAvailableToOrderByRouteID(Guid routeID);

        ICollection<Box> GetBoxesDeliveredByRouteID(Guid orderID);
    }
}
