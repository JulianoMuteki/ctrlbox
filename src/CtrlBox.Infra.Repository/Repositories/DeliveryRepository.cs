using CtrlBox.CrossCutting;
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
    public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(CtrlBoxContext context)
            : base(context)
        {

        }

        public Delivery GetResumeDeliveryById(Guid deliveryID)
        {
            try
            {
                return _context.Set<Delivery>()
                    .Include(x => x.DeliveriesProducts).ThenInclude(p => p.Product)
                    .Include(x=>x.Sales).ThenInclude(s => s.SalesProducts)
                    .Where(x => x.Id == deliveryID)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryRepository>("Unexpected error fetching get deliveries", nameof(this.GetDeliveryByUserWithRoute), ex);
            }
        }

        public ICollection<Delivery> GetDeliveryByUserWithRoute(Guid userId)
        {
            try
            {
                return _context.Set<Delivery>().Include(x => x.Route).Include(x => x.User).Where(x => x.UserID == userId).ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryRepository>("Unexpected error fetching get deliveries", nameof(this.GetDeliveryByUserWithRoute), ex);
            }
        }

        public ICollection<DeliveryProduct> GetDeliveryProductsLoad(Guid deliveryID)
        {
            try
            {
                return _context.Set<DeliveryProduct>().Include(x => x.Delivery).Include(x => x.Product).Where(x => x.DeliveryID == deliveryID).ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryRepository>("Unexpected error fetching get deliveries", nameof(this.GetDeliveryProductsLoad), ex);
            }
        }

        public ICollection<Delivery> GetDeliveryRouteLoad()
        {
            try
            {
                return _context.Set<Delivery>().Include(x => x.User).Include(x => x.Route).ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryRepository>("Unexpected error fetching get deliveries", nameof(this.GetDeliveryRouteLoad), ex);
            }
        }
    }
}
