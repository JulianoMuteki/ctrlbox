﻿using CtrlBox.CrossCutting.Barcode;
using CtrlBox.Domain.Common;
using System;

namespace CtrlBox.Domain.Entities
{
    public class BoxCode : EntityBase
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
        public Box Box { get; set; }

        public BoxCode()
            : base()
        {
            this.BarcodeEAN13 = BarcodeGenerator.GetBarCodeNumber();
            this.BarcodeGS1_128 = string.Empty;
            this.RFID = string.Empty;

        }
    }
}
