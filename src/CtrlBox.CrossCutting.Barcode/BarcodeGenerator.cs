using System;
using System.Linq;

namespace CtrlBox.CrossCutting.Barcode
{
    public class BarcodeGenerator
    {
        static Random _random;
        //public BarcodeGenerator()
        //{
        //    _random = new Random();

        //   // Barcode = Criar_EAN("789", "001", RandomNumber(0, 999999).ToString("D6"));
        //}

        public static string GetBarCodeNumber()
        {
            _random = new Random();

            return Criar_EAN("789", RandomNumber(0, 999).ToString("D3"), RandomNumber(0, 999999).ToString("D6"));
        }

        private static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        private static string Criar_EAN(string sCountry, string SCompany, string sCode)

        {

            string sParte = "";
            int[] vSequen = new int[] { 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3 };
            int[] vResult = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int iDigito, iSoma, iMultiplo = 0;

            sParte = sCountry + SCompany + sCode;

            for (int i = 0; i < sParte.Length; i++)
            {
                vResult[i] = vSequen[i] * (Convert.ToInt16(sParte[i]) - 48);
            }

            iSoma = vResult.Sum();

            if (iSoma % 10 != 0 && iSoma > 10)
                iMultiplo = ((iSoma / 10) + 1);
            else
                if (iSoma < 10)
                iMultiplo = 1;
            else
                iMultiplo = iSoma / 10;

            iDigito = (iMultiplo * 10) - iSoma;

            return sParte + iDigito.ToString();
        }
    }

}
