﻿using CtrlBox.Domain.Common;
using System;

namespace CtrlBox.Domain.Entities
{
    public class DeliveryProduct : ValueObject<DeliveryProduct>
    {
        public Guid DeliveryID { get; set; }
        public Guid ProductID { get; set; }

        public int Amount { get; set; }

        protected override bool EqualsCore(DeliveryProduct other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}