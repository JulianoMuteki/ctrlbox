using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class ClientCategoryVM
    {
        public Guid ClientID { get; set; }
        public ClientVM Client { get; set; }

        public Guid CategoryID { get; set; }
        public CategoryVM Category { get; set; }
    }
}
