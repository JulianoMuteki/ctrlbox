using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class Stock: EntityBase
    {
        public int AmountBoxes { get; set; }

        public ICollection<StockProduct> StocksProducts { get; set; }

        public Stock()
        {
            this.Id = Guid.NewGuid();
            this.CreationDate = DateTime.Now;
            this.DateModified = DateTime.Now;

            this.StocksProducts = new HashSet<StockProduct>();
        }
    }
}
