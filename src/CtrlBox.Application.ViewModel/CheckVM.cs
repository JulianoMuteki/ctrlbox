using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class CheckVM : ViewModelBase
    {
        public Guid SaleID { get; set; }
        public int Number { get; set; }
        public float Value { get; set; }
        public DateTime? DtExpire
        {
            get
            { return DateTime.ParseExact(DateCheck, "yyyy-MM-dd HH:mm tt", null); }
        }

        public string DateCheck { get; set; }
        public SaleVM Sale { get; set; }
    }
}
