﻿using System;
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
                var query = _context.Set<ProductItem>()
                              .Where(x => x.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.Available &&
                                     x.ProductID == productID)
                              .Count();

                return query;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductRepository>("Unexpected error fetching total", nameof(this.GetTotalProductItemByProductID), ex);
            }
        }

        public ICollection<ProductItem> GetAvailableProductItemByProductID(Guid productID, int quantity)
        {
            try
            {
                var query = _context.Set<ProductItem>()
                              .Where(x => x.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.Available &&
                                     x.ProductID == productID)
                              .Take(quantity);

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductRepository>("Unexpected error fetching total", nameof(this.GetTotalProductItemByProductID), ex);
            }
        }

        public ICollection<ProductItem> GetAvailableStockProductItemsByClientIDAndProductID(Guid productID, Guid clientID)
        {
            try
            {
                var query = _context.Set<ProductItem>()
                              .Where(x => x.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.InStock &&
                                     x.ProductID == productID)
                            //.Join(_context.Set<Tracking>(),
                            //      pdi => pdi.Id,
                            //      track => track.ProductItemID,
                            //      (pdi, track) => new { ProductItem = pdi, Tracking = track })
                            //.Join(_context.Set<TrackingType>(),
                            //      track => track.Tracking.TrackingTypeID,
                            //      tt => tt.Id,
                            //      (track, tt) => new { track.Tracking, TrackingType = tt })
                            //.Join(_context.Set<TrackingClient>(),
                            //      track => track.Tracking.Id,
                            //      cl => cl.TrackingID,
                            //      (tr, trcl) => new { tr.ProductItem, tr.Tracking, TrackingClient = trcl, tr.TrackingType })
                           
                            //  .Where(x => x.TrackingType.TrackType == CrossCutting.Enums.ETrackType.Place &&
                            //         x.ProductItem.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.InStock &&
                            //         x.ProductItem.ProductID == productID &&
                            //         x.TrackingClient.ClientID == clientID)
                           //.Select(x => x.ProductItem);

                .Select(x => x);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductRepository>("Unexpected error fetching total", nameof(this.GetTotalProductItemByProductID), ex);
            }
        }

        public ICollection<Box> GetBoxesInStockByBoxTypeIDAndClientID(Guid boxTypeID, Guid clientID)
        {
            try
            {
                var query = _context.Set<Box>();
                           //   .Where(x => x.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.InStock &&
                           //          x.BoxTypeID == boxTypeID)
                           //   .Join(_context.Set<Tracking>(),
                           //   pdi => pdi.Id,
                           //   track => track.BoxID,
                           //   (pdi, track) => new { Box = pdi, Tracking = track })
                           // .Join(_context.Set<TrackingType>(),
                           //   track => track.Tracking.TrackingTypeID,
                           //   tt => tt.Id,
                           //   (track, tt) => new { track.Box, track.Tracking, TrackingType = tt })
                           // .Join(_context.Set<TrackingClient>(),
                           //   track => track.Tracking.Id,
                           //   cl => cl.TrackingID,
                           //   (tr, trcl) => new { tr.Box, tr.Tracking, TrackingClient = trcl, tr.TrackingType })
                           //   .Where(x => x.TrackingType.TrackType == CrossCutting.Enums.ETrackType.Place &&
                           //          x.Box.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.InStock &&
                           //          x.Box.BoxTypeID == boxTypeID &&
                           //          x.TrackingClient.ClientID == clientID)
                           //.Select(x => x.Box);

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductRepository>("Unexpected error fetching total", nameof(this.GetTotalProductItemByProductID), ex);
            }
        }

        public ICollection<Box> GetBoxesInStockByClientID(Guid clientID)
        {
            try
            {
                var query = _context.Set<Box>();
                           //   .Where(x => (x.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.InStock || x.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.CrossDocking) && x.BoxParentID == null)
                           //   .Join(_context.Set<Tracking>(),
                           //   pdi => pdi.Id,
                           //   track => track.BoxID,
                           //   (pdi, track) => new { Box = pdi, Tracking = track })
                           // .Join(_context.Set<TrackingType>(),
                           //   track => track.Tracking.TrackingTypeID,
                           //   tt => tt.Id,
                           //   (track, tt) => new { track.Box, track.Tracking, TrackingType = tt })
                           // .Join(_context.Set<TrackingClient>(),
                           //   track => track.Tracking.Id,
                           //   cl => cl.TrackingID,
                           //   (tr, trcl) => new { tr.Box, tr.Tracking, TrackingClient = trcl, tr.TrackingType })
                           //   .Where(x => x.TrackingType.TrackType == CrossCutting.Enums.ETrackType.Place &&
                           //          (x.Box.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.InStock || x.Box.FlowStep.EFlowStep == CrossCutting.Enums.EFlowStep.CrossDocking) &&
                           //          x.TrackingClient.ClientID == clientID && x.Tracking.IsLastTrack)
                           //.Select(x => x.Box)
                           //.Include(x => x.BoxType).ThenInclude(x => x.Picture);
                           

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductRepository>("Unexpected error fetching total", nameof(this.GetTotalProductItemByProductID), ex);
            }
        }

        public ICollection<Stock> GetStocks()
        {
            try
            {
                var query = _context.Set<Stock>()
                             .Include(x => x.Product).ThenInclude(z => z.Picture)
                             .Include(x => x.Client);

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductRepository>("Unexpected error fetching GetStocks", nameof(this.GetStocks), ex);
            }
        }

        public ICollection<StockMovement> GetStocksMovements(Guid stockID)
        {
            try
            {
                var query = _context.Set<StockMovement>()
                             .Include(x => x.Stock).ThenInclude(z => z.Product).ThenInclude(p => p.Picture)
                             .Include(x => x.ClientSupplier)
                             .Where(x => x.StockID == stockID);

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductRepository>("Unexpected error fetching GetStocks", nameof(this.GetStocks), ex);
            }
        }
    }
}
