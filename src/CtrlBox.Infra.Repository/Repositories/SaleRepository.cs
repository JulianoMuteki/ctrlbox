﻿using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CtrlBox.Infra.Repository.Repositories
{
   public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(CtrlBoxContext context)
            : base(context)
        {

        }

        public Sale GetInvoiceSaleByID(Guid saleID)
        {
            try
            {
                return _context.Set<Sale>().Include(x => x.SalesProducts).Include(x=>x.SalesProducts).ThenInclude(p => p.Product).Where(x => x.Id == saleID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<SaleRepository>("Unexpected error fetching get deliveries", nameof(this.GetInvoiceSaleByID), ex);
            }
        }
    }
}
