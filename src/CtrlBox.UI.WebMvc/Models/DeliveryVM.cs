using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.Models
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
}