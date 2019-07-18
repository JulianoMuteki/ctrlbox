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
                    .Include(x => x.BoxType)
                    .Where(x=>x.BoxParentID == null)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetAll", nameof(this.GetBoxesParentsWithBoxType), ex);
            }
        }

        public ICollection<Box> GetBoxesByDeliveryWithBoxType(Guid deliveryID)
        {
            try
            {
                var query = _context.Set<Box>()    // your starting point - table in the "from" statement
                           .Join(_context.Set<DeliveryBox>(), // the source table of the inner join
                              box => box.Id,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                              bDel => bDel.BoxID,   // Select the foreign key (the second part of the "on" clause)
                              (box, deliveryBox) => new { Box = box, DeliveryBox = deliveryBox }) // selection
                           .Where(x => x.DeliveryBox.DeliveryID == deliveryID)    // where statement
                           .Select(x => x.Box)
                           .Include(b => b.BoxType);

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

        public ICollection<BoxProductItem> GetBoxesBoxesProductItemsByDeliveryID(Guid deliveryID)
        {
            try
            {
                var query = _context.Set<BoxProductItem>()
                            .Where(x => x.DeliveryID == deliveryID)
                            .Include(b => b.ProductItem).ThenInclude(p => p.Product);

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching Get boxes with product items", nameof(this.GetBoxesByBoxTypeIDWithProductItems), ex);
            }
        }
    }
}
