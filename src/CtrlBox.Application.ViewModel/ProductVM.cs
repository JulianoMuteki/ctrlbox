﻿using System.Collections.Generic;
using System;

namespace CtrlBox.Application.ViewModel
{
    public class ProductVM : ViewModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Package { get; set; }
        public float Capacity { get; set; }
        public string UnitMeasure { get; set; }
        public string UnitType { get; set; }
        public float Weight { get; set; }
        public string MassUnitWeight { get; set; }

        public Guid? PictureID { get; set; }
        public PictureVM Picture { get; set; }

        public IList<string> OptionsMassUnit { get; set; }
        public IList<string> OptionsVolumeUnit { get; set; }

        public string FormattedProduct
        {
            get
            {
                return $"{this.Name} - {this.Description} - {this.Package} - {this.Capacity}{this.UnitMeasure}";
            }
        }

        public ICollection<ClientProductValueVM> CustomersProductsValues { get; set; }
        public ICollection<DeliveryDetailVM> DeliveriesProducts { get; set; }
        public ICollection<SaleProductVM> SalesProducts { get; set; }

        public ProductVM()
        {
            this.DeliveriesProducts = new HashSet<DeliveryDetailVM>();
            this.SalesProducts = new HashSet<SaleProductVM>();
            this.CustomersProductsValues = new HashSet<ClientProductValueVM>();
        }
    }
}
