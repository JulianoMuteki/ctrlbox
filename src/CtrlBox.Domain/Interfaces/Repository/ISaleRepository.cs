using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;
using System;

namespace CtrlBox.Domain.Interfaces.Repository
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Sale GetInvoiceSaleByID(Guid saleID);
    }
}
