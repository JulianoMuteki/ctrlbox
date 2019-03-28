using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModels
{
    public class DeliveryVM : ViewModelBase
    {
        public Guid RouteID { get; set; }

        public bool IsFinalized { get; set; }
        public DateTime DtStart { get; set; }
        public DateTime? DtEnd { get; set; }
        public string CreatedBy { get; set; }
        public string FinalizedBy { get; set; }

        public RouteVM RouteVM { get; set; }

        public ICollection<DeliveryProductVM> DeliveriesProducts { get; set; }

        public DeliveryVM()
        {
            this.DeliveriesProducts = new HashSet<DeliveryProductVM>();
        }
    }

    public class DeliveryProductVM
    {
        public Guid DeliveryID { get; set; }
        public Guid ProductID { get; set; }

        public int Amount { get; set; }
    }
}l
