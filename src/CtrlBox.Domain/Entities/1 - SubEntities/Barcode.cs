using CtrlBox.CrossCutting.Barcode;
using CtrlBox.Domain.Common;

namespace CtrlBox.Domain.Entities._0___SubEntities
{
    public class Barcode : EntityBase
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

        private Barcode()
            : base()
        {
            this.BarcodeEAN13 = BarcodeGenerator.GetBarCodeNumber();
            this.BarcodeGS1_128 = string.Empty;
            this.RFID = string.Empty;
        }

        public static Barcode FactoryCreate()
        {
            return new Barcode();
        }
    }
}
