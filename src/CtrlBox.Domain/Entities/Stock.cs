using CtrlBox.Domain.Common;
using System;

namespace CtrlBox.Domain.Entities
{
    public class Stock: EntityBase
    {
        public Client StorageLocation { get; private set; }
        public Product Product { get; private set; }

        public Guid StorageLocationID { get; private set; }
        public Guid ProductID { get; private set; }
        public int Minimum { get; private set; }
        public int TotalStock { get; private set; }
        public decimal DefaultPrice { get; private set; }

        public Stock()
            :base()
        {

        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }
    }
}
