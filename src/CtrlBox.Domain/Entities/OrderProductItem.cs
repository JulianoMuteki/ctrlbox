﻿using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class OrderProductItem : ValueObject<OrderProductItem>
    {
        public Guid ProductItemID { get; set; }
        public ProductItem ProductItem { get; set; }

        public Guid OrderID { get; set; }
        public Order Order { get; set; }

        private OrderProductItem()
        {

        }

        public static OrderProductItem FactoryCreate(Guid productItemID, Guid orderID)
        {
            return new OrderProductItem()
            {
                ProductItemID = productItemID,
                OrderID = orderID
            };
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}