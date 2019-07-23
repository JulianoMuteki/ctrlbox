using System;
using System.Collections.Generic;
using UnitsNet;
using UnitsNet.Units;

namespace CtrlBox.CrossCutting
{
    public static class CtrlBoxUnits
    {
        //https://github.com/angularsen/UnitsNet

        public static IList<string> CtrlBoxMassUnit { get { return GetMassUnit(); } }
        public static IList<string> CtrlBoxVolumeUnit { get { return GetVolumeUnit(); } }
        public static IList<string> CtrlBoxUnitType { get { return GetTypesUnit(); } }


        private static IList<String> GetMassUnit()
        {
            IList<String> unitsMass = new List<string>();
            foreach (var mass in Enum.GetNames(typeof(MassUnit)))
            {
                unitsMass.Add(mass);
            }

            return unitsMass;
        }
        private static IList<String> GetTypesUnit()
        {
            IList<String> unitsType = new List<string>();
            unitsType.Add(QuantityType.Volume.ToString());
            unitsType.Add(QuantityType.Mass.ToString());
            unitsType.Add(QuantityType.Length.ToString());
            //foreach (var type in Quantity.Types)
            //{
            //    unitsType.Add(type);
            //}

            return unitsType;
        }

        private static IList<String> GetLengthUnit()
        {
            IList<String> volumeUnit = new List<string>();
            foreach (var volume in Enum.GetNames(typeof(LengthUnit)))
            {
                volumeUnit.Add(volume);
            }

            return volumeUnit;
        }


        private static IList<String> GetVolumeUnit()
        {
            IList<String> volumeUnit = new List<string>();
            foreach (var volume in Enum.GetNames(typeof(VolumeUnit)))
            {
                volumeUnit.Add(volume);
            }

            return volumeUnit;
        }
    }
}
