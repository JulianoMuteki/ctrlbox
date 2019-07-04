using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Application.ViewModel
{
    public class AddressVM : ViewModelBase
    {
        public string CEP { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string Estate { get; set; }
        
        private string _reference;

        public string Reference
        {
            get { return _reference ?? string.Empty; }
            set { _reference = value; }
        }

    }
}
