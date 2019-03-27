using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.Models
{
    public class ClientProductValueVM
    {
        public Guid ProductID { get; set; }

        public Guid ClientID { get; set; }

        public float Price { get; set; }
    }
}