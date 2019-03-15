using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class Check : EntityBase
    {
        public Guid SaleID { get; set; }
        public int Number { get; set; }
        public float Value { get; set; }
        public DateTime DtExpire { get; set; }
    }
}
