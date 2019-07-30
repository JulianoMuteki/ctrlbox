using System.Collections.Generic;

namespace CtrlBox.Application.ViewModel
{
    public class OptiontTypeVM: ViewModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string EClientType { get; set; }

        public ICollection<ClientOptionTypeVM> ClientsOptionsTypes { get; set; }
    }
}
