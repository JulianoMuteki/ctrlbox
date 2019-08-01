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
    public class DeliveryRepository : GenericRepository<Order>, IDeliveryRepository
    {
        public DeliveryRepository(CtrlBoxContext context)
            : base(context)
        {

        }

        public Order GetResumeDeliveryById(Guid deliveryID)
        {
            try
            {
                return _context.Set<Order>()
                    .Include(x => x.DeliveriesDetails).ThenInclude(p => p.Product)
                    .Include(x=>x.Sales).ThenInclude(s => s.SalesProducts)
                    .Where(x => x.Id == deliveryID)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryRepository>("Unexpected error fetching get deliveries", nameof(this.GetDeliveryByUserWithRoute), ex);
            }
        }

        public ICollection<Order> GetDeliveryByUserWithRoute(Guid userId)
        {
            try
            {
                return _context.Set<Order>().Include(x => x.Route).Include(x => x.User).Where(x => x.UserID == userId).ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryRepository>("Unexpected error fetching get deliveries", nameof(this.GetDeliveryByUserWithRoute), ex);
            }
        }

        public ICollection<DeliveryDetail> GetDeliveryProductsLoad(Guid deliveryID)
        {
            try
            {
                return _context.Set<DeliveryDetail>().Include(x => x.Order).Include(x => x.Product).Where(x => x.OrderID == deliveryID).ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryRepository>("Unexpected error fetching get deliveries", nameof(this.GetDeliveryProductsLoad), ex);
            }
        }

        public ICollection<Order> GetDeliveryRouteLoad()
        {
            try
            {
                return _context.Set<Order>().Include(x => x.User).Include(x => x.Route).ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryRepository>("Unexpected error fetching get deliveries", nameof(this.GetDeliveryRouteLoad), ex);
            }
        }
    }
}
