﻿using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Infra.Repository.Repositories
{
    public class DeliveryRepository: GenericRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(CtrlBoxContext context)
            :base(context)
        {

        }

        public ICollection<DeliveryProduct> GetDeliveryProductsLoad(Guid deliveryID)
        {
            return _context.Set<DeliveryProduct>().Include(x=>x.Delivery).Include(x => x.Product).Where(x => x.DeliveryID == deliveryID).ToList();
        }
    }
}
