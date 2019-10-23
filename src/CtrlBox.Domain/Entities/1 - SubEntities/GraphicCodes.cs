using CtrlBox.CrossCutting.Barcode;
using CtrlBox.Domain.Common;

namespace CtrlBox.Domain.Entities
{
    public class GraphicCodes : EntityBase
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

        private GraphicCodes()
            : base()
        {
            this.BarcodeEAN13 = BarcodeGenerator.GetBarCodeNumber();
            this.BarcodeGS1_128 = string.Empty;
            this.RFID = string.Empty;
        }

        public static GraphicCodes FactoryCreate(string barcode)
        {
            GraphicCodes gc = new GraphicCodes();
            gc.BarcodeEAN13 = barcode;

            return gc;
        }

        public static GraphicCodes FactoryCreate()
        {
            return new GraphicCodes();
        }
    }
}
