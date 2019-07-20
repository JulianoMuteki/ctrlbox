﻿using System;
namespace CtrlBox.Application.ViewModel
{
    public class BoxCodeVM : ViewModelBase
    {
        /// <summary>
        /// 13 digits
        /// </summary>
        public string BarcodeEAN13 { get; set; }

        /// <summary>
        /// 48 digits
        /// </summary>
        public string BarcodeGS1_128 { get; set; }

        public string RFID { get; set; }

        public Guid BoxID { get; set; }
        public BoxVM Box { get; set; }
    }
}
