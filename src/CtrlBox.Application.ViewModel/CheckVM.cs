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
            {
                DateTime? dt = DateTime.Parse(DateCheck);
                return dt;
            }
        }

        public string DateCheck { get; set; }
        public SaleVM Sale { get; set; }
    }
}
