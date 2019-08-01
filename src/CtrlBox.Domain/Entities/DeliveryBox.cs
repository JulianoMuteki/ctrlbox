﻿using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class DeliveryBox : ValueObject<DeliveryBox>
    {
        public Guid BoxID { get; set; }
        public Guid DeliveryID { get; set; }

        public Delivery Delivery { get; set; }
        public Box Box { get; set; }

        public Guid ClientID { get; set; }
        public Client Client { get; set; }

        public DeliveryBox()
        {

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.GetType().GetProperties().Select(propInfo => propInfo.GetValue(this, null));
        }
    }
}
