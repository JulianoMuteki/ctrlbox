using CtrlBox.Domain.Entities;
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

        public ICollection<Delivery> GetDeliveryByUserWithRoute(Guid userId)
        {
            return _context.Set<Delivery>().Include(x => x.Route).Include(x=>x.User).Where(x => x.UserID == userId).ToList();
        }

        public ICollection<DeliveryProduct> GetDeliveryProductsLoad(Guid deliveryID)
        {
            return _context.Set<DeliveryProduct>().Include(x=>x.Delivery).Include(x => x.Product).Where(x => x.DeliveryID == deliveryID).ToList();
        }

        public ICollection<Delivery> GetDeliveryRouteLoad()
        {
            return _context.Set<Delivery>().Include(x => x.User).Include(x => x.Route).ToList();
        }
    }
}
