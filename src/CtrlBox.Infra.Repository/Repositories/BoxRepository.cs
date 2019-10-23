using System;
using System.Collections.Generic;
using System.Linq;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace CtrlBox.Infra.Repository.Repositories
{
    public class BoxRepository : GenericRepository<Box>, IBoxRepository
    {
        public BoxRepository(CtrlBoxContext context)
            : base(context)
        {

        }

        public ICollection<Box> GetAllWithBoxTypeAndProduct()
        {
            try
            {
                return _context.Set<Box>()
                    .Include(x => x.BoxType)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetAll", nameof(this.GetAllWithBoxTypeAndProduct), ex);
            }
        }

        public ICollection<Box> GetBoxesParentsWithBoxTypeEndProduct()
        {
            try
            {
                return _context.Set<Box>()
                    .Where(x => x.BoxParentID == null)
                    .Include(x => x.BoxType)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetAll", nameof(this.GetAllWithBoxTypeAndProduct), ex);
            }
        }

        public ICollection<Box> GetBoxesParentsWithBoxType()
        {
            try
            {
                return _context.Set<Box>()
                    .Include(x => x.BoxType).ThenInclude(p=>p.Picture)
                    
                    .Where(x => x.BoxParentID == null)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetAll", nameof(this.GetBoxesParentsWithBoxType), ex);
            }
        }

        public ICollection<Box> GetBoxesByDeliveryIDWithProductItems(Guid deliveryID)
        {
            try
            {
                var query = _context.Set<Box>()
                           .Include(b => b.BoxType)
                           .Include(x => x.BoxesChildren)
                           .Include(b => b.BoxesProductItems).ThenInclude(x=>x.ProductItem)
                           .AsEnumerable() // <-- Force full execution (loading)
                           .Join(_context.Set<OrderBox>(), // the source table of the inner join
                              box => box.Id,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                              bDel => bDel.BoxID,   // Select the foreign key (the second part of the "on" clause)
                              (box, deliveryBox) => new { Box = box, DeliveryBox = deliveryBox }) // selection                      

                              .Where(x => x.DeliveryBox.OrderID == deliveryID)
                           .Select(x => x.Box);


                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetAll", nameof(this.GetBoxesParentsWithBoxType), ex);
            }
        }

        public ICollection<Box> GetBoxesByDeliveryWithBoxType(Guid orderID)
        {
            try
            {
                var query = _context.Set<Box>()
                           .Include(b => b.BoxType)
                           .Include(x => x.BoxesChildren)
                            .Include(b => b.BoxesProductItems).ThenInclude(z => z.ProductItem)
                            .AsEnumerable() // <-- Force full execution (loading) of the above
                              .Where(x => (x.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.Expedition) && x.BoxParentID == null)
                           .Join(_context.Set<OrderBox>(), // the source table of the inner join
                              box => box.Id,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                              bDel => bDel.BoxID,   // Select the foreign key (the second part of the "on" clause)
                              (box, orderBox) => new { Box = box, OrderBox = orderBox }) // selection                      
                           
                           .Where(x => x.OrderBox.OrderID == orderID)
                           .Select(x => x.Box);
                           

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetAll", nameof(this.GetBoxesParentsWithBoxType), ex);
            }
        }

        public ICollection<Box> GetBoxesByBoxTypeIDWithProductItems(Guid boxTypeID, int quantity)
        {
            try
            {
                var query = _context.Set<Box>()

                            .Include(x => x.BoxesChildren)
                            .Include(b => b.BoxesProductItems).ThenInclude(z => z.ProductItem)
                            .AsEnumerable() // <-- Force full execution (loading) of the above
                            .Where(x => x.BoxTypeID == boxTypeID && x.BoxParent == null)
                            .OrderByDescending(x => x.DateModified)
                            .Take(quantity);

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching Get boxes with product items", nameof(this.GetBoxesByBoxTypeIDWithProductItems), ex);
            }
        }

        public ICollection<Box> GetBoxesByBoxWithChildren(Guid boxID)
        {
            try
            {
                var query = _context.Set<Box>()
                            .Include(x => x.BoxType).ThenInclude(p => p.Picture)
                            //.Include(x => x.Product).ThenInclude(z => z.Picture)

                            .Include(x => x.BoxesChildren)
                            .Include(b => b.BoxesProductItems).ThenInclude(z => z.ProductItem)
                            .AsEnumerable() // <-- Force full execution (loading) of the above
                            .Where(x => x.Id == boxID && x.BoxParent == null);

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching Get boxes with product items", nameof(this.GetBoxesByBoxTypeIDWithProductItems), ex);
            }
        }

        public ICollection<ProductItem> GetOrderProductItemByDeliveryID(Guid orderID)
        {
            try
            {
                var query = _context.Set<ProductItem>()
                            .Include(b => b.Product)

                            .Include(x => x.BoxesProductItems).ThenInclude(b => b.Box).ThenInclude(bt => bt.BoxType)
                           .Join(_context.Set<OrderProductItem>(),
                              ordP => ordP.Id,
                              pi => pi.ProductItemID,
                              (productItem, orderProductItem) => new { ProductItem = productItem, OrderProductItem = orderProductItem })

                           .Where(x => x.OrderProductItem.OrderID == orderID && x.ProductItem.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.Expedition)
                           .Select(x => x.ProductItem);

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching Get boxes with product items", nameof(this.GetBoxesByBoxTypeIDWithProductItems), ex);
            }
        }

        public Box GetBoxesByIDWithBoxTypeAndProductItems(Guid boxID)
        {
            try
            {
                return _context.Set<Box>()
                   // .Include(x=>x.BoxBarcode)
                    .Include(x => x.BoxType).ThenInclude(x => x.Picture)
                    .Where(x => x.Id == boxID)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetBoxesByIDWithBoxTypeAndProductItems", nameof(this.GetBoxesByIDWithBoxTypeAndProductItems), ex);
            }
        }

        public ICollection<Box> GetBoxesParentsByOrderIDWithProductItems(Guid orderID)
        {
            try
            {
                IEnumerable<Box> query = GetBoxesFullByOrderAndFlowStep(orderID, CrossCutting.Enums.EFlowStep.Expedition);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetAll", nameof(this.GetBoxesParentsWithBoxType), ex);
            }
        }

        private IEnumerable<Box> GetBoxesFullByOrderAndFlowStep(Guid orderID, CrossCutting.Enums.EFlowStep eFlowStep)
        {
            var query = _context.Set<Box>()
                       .Include(b => b.BoxType)
                       .Include(x => x.BoxesChildren)
                       .Include(b => b.BoxesProductItems).ThenInclude(x => x.ProductItem)
                       .Include(x=>x.Trackings)
                       .AsEnumerable() // <-- Force full execution (loading)
                       .Join(_context.Set<OrderBox>(), // the source table of the inner join
                          box => box.Id,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                          bDel => bDel.BoxID,   // Select the foreign key (the second part of the "on" clause)
                          (box, deliveryBox) => new { Box = box, DeliveryBox = deliveryBox }) // selection                      

                       .Where(x => x.DeliveryBox.OrderID == orderID && x.Box.BoxParentID == null && x.Box.FlowStep.EFlowStep == eFlowStep)
                       .Select(x => x.Box);
            return query;
        }

        public ICollection<Box> GetBoxesAvailableToOrderByRouteID(Guid routeID)
        {
            try
            {
                var query = _context.Set<Box>()
                            .Include(x => x.BoxesChildren)
                            .Include(b => b.BoxesProductItems).ThenInclude(z => z.ProductItem)
                            .AsEnumerable() // <-- Force full execution (loading) of the above
                              .Where(x => (x.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.InStock || x.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.CrossDocking) && x.BoxParentID == null)
                            .Join(_context.Set<Tracking>(),
                              pdi => pdi.Id,
                              track => track.BoxID,
                              (pdi, track) => new { Box = pdi, Tracking = track })
                            .Join(_context.Set<TrackingType>(),
                              track => track.Tracking.TrackingTypeID,
                              tt => tt.Id,
                              (track, tt) => new { track.Box, track.Tracking, TrackingType = tt })
                            .Join(_context.Set<TrackingClient>(),
                              track => track.Tracking.Id,
                              cl => cl.TrackingID,
                              (tr, trcl) => new { tr.Box, tr.Tracking, TrackingClient = trcl, tr.TrackingType })
                            .Join(_context.Set<Route>(),
                              rt => rt.TrackingClient.ClientID,
                              clit => clit.ClientOriginID,
                              (tr, rt) => new { tr.Box, tr.Tracking, tr.TrackingClient, tr.TrackingType, Route = rt })
                              .Where(x => x.TrackingType.TrackType == CrossCutting.Enums.ETrackType.Place &&
                                     (x.Box.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.InStock || x.Box.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.CrossDocking) &&
                                     x.Route.Id == routeID)
                           .Select(x => x.Box);                          

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductRepository>("Unexpected error fetching total", nameof(this.GetBoxesAvailableToOrderByRouteID), ex);
            }
        }

        public ICollection<Box> GetBoxesDeliveredByRouteID(Guid orderID)
        {
            try
            {
                IEnumerable<Box> query = GetBoxesFullByOrderAndFlowStep(orderID, CrossCutting.Enums.EFlowStep.Delivered);

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetAll", nameof(this.GetBoxesDeliveredByRouteID), ex);
            }
        }
    }
}
