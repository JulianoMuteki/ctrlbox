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
                    .Include(x => x.Product)
                    .Include(x => x.BoxType)
                    .Include(x => x.BoxBarcode)
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
                    .Include(x => x.Product)
                    .Include(x => x.BoxType)
                    .Include(x=>x.BoxBarcode)
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
                           .Include(b => b.BoxesProductItems)
                           .AsEnumerable() // <-- Force full execution (loading)
                           .Join(_context.Set<OrderBox>(), // the source table of the inner join
                              box => box.Id,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                              bDel => bDel.BoxID,   // Select the foreign key (the second part of the "on" clause)
                              (box, orderBox) => new { Box = box, OrderBox = orderBox }) // selection                      
                           
                           .Where(x => x.OrderBox.OrderID == orderID && x.Box.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.Order)
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
                            .Include(x => x.Product).ThenInclude(z => z.Picture)

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

        public ICollection<OrderProductItem> GetOrderProductItemByDeliveryID(Guid orderID)
        {
            try
            {
                //var query = _context.Set<OrderProductItem>()
                //            .Include(b => b.ProductItem).ThenInclude(p => p.Product)
                //            .Where(x => x.OrderID == deliveryID);
                var query = _context.Set<OrderProductItem>()
                            .Include(b => b.ProductItem).ThenInclude(p => p.Product)
                           //.Join(_context.Set<Box>(),
                           //   ordP => ordP.ProductItemID,
                           //   box => box.Id,
                           //   (orderProductItem, box) => new { Box = box, OrderProductItem = orderProductItem })               

                           .Where(x => x.OrderID == orderID && x.ProductItem.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.Order);
                          

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
                    .Include(x=>x.BoxBarcode)
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
                var query = _context.Set<Box>()
                           .Include(b => b.BoxType)
                           .Include(x => x.BoxesChildren)
                           .Include(b => b.BoxesProductItems).ThenInclude(x => x.ProductItem)
                           .AsEnumerable() // <-- Force full execution (loading)
                           .Join(_context.Set<OrderBox>(), // the source table of the inner join
                              box => box.Id,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                              bDel => bDel.BoxID,   // Select the foreign key (the second part of the "on" clause)
                              (box, deliveryBox) => new { Box = box, DeliveryBox = deliveryBox }) // selection                      

                              .Where(x => x.DeliveryBox.OrderID == orderID && x.Box.BoxParentID == null && x.Box.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.Order)
                           .Select(x => x.Box);


                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetAll", nameof(this.GetBoxesParentsWithBoxType), ex);
            }
        }
    }
}
