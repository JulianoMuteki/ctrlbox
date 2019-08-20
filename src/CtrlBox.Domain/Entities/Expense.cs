using CtrlBox.Domain.Common;
using System;

namespace CtrlBox.Domain.Entities
{
    public class Expense : EntityBase
    {
        public Guid OrderID { get; set; }

        public double Value { get; set; }

        public string Description { get; set; }

        public Order Order { get; set; }

        public Expense()
        {

        }
    }
}
