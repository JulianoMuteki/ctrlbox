using System;
using System.Linq;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace CtrlBox.Infra.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(CtrlBoxContext context)
            : base(context)
        {

        }

        public int GetTotalProductItemByProductID(Guid productID)
        {
            try
            {
                //var query = _context.Set<ProductItem>()
                //           .Include(b => b.Product)
                //           .Join(_context.Set<Tracking>(),
                //              pdi => pdi.Id,
                //              track => track.ProductItemID,
                //              (pdi, track) => new { ProductItem = pdi, Tracking = track })
                //            .Join(_context.Set<TrackingClient>(),
                //              track => track.Tracking.Id,
                //              cl => cl.TrackingID,
                //              (tr, trcl) => new { tr.ProductItem, tr.Tracking, TrackingClient = trcl })
                //            .Include(x => x.Tracking.TrackingType)
                //              .Where(x => x.Tracking.TrackingType.TrackType == CrossCutting.Enums.ETrackType.Place &&
                //                     x.ProductItem.EFlowStep == CrossCutting.Enums.EFlowStep.Create &&
                //                     x.ProductItem.ProductID == productID &&
                //                     x.TrackingClient.ClientID == clientID)
                //           .Select(x => x.ProductItem);
                var query = _context.Set<ProductItem>()
                              .Where(x => x.EFlowStep == CrossCutting.Enums.EFlowStep.Create &&
                                     x.ProductID == productID);

                return query.ToList().Count();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductRepository>("Unexpected error fetching total", nameof(this.GetTotalProductItemByProductID), ex);
            }
        }
    }
}
