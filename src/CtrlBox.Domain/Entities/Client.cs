using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Entities
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public int QuantityBoxes { get; set; }
        public float BalanceDue { get; set; }

    }
}
