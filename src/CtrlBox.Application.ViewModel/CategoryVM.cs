using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class CategoryVM : ViewModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ClientCategoryVM> ClientsCategories { get; set; }
    }
}
