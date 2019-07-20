using System;
using System.Linq;

namespace CtrlBox.CrossCutting.Barcode
{
    public class Class1
    {
        Random _random = new Random();

        public Class1()
        {
            //Install-Package BarcodeLib -Version 2.2.2
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();

            var L = "789100031550".Split();//  'Converte string em Array de Caracteres
                                           //For i = 0 to L.Ubound
                                           //    RE = RE + L(i).Val * ((i MOD 2) *2 + 1)  ' soma todos multiplicando apenas os pares por 3
                                           //Next

           var barCodeBox = Criar_EAN("789", "001", RandomNumber(0, 999999).ToString("D9"));
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static string Criar_EAN(string sPais, string sEmpresa, string sCodigo)

        {

            string sParte = "";
            int[] vSequen = new int[] { 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3 };
            int[] vResult = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int iDigito, iSoma, iMultiplo = 0;

            sParte = sPais + sEmpresa + sCodigo;

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
