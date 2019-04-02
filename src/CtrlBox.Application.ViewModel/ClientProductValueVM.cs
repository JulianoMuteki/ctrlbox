using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class ClientProductValueVM
    {
        public Guid ClientID { get; set; }
        public Guid ProductID { get; set; }
        public ProductVM Product { get; set; }
        public ClientVM Client { get; set; }

        public float Price { get; set; }
    }
}
