using System;

namespace CtrlBox.Application.ViewModel
{
    public class ClientOptionTypeVM
    {
        public Guid ClientID { get; set; }
        public ClientVM Client { get; set; }

        public Guid OptiontTypeID { get; set; }
        public OptiontTypeVM OptiontType { get; set; }
    }
}
