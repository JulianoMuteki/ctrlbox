using CtrlBox.CrossCutting;
using CtrlBox.Domain.Common;
using CtrlBox.Domain.Validations;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Package { get; set; }
        public float Capacity { get; set; }
        public string UnitMeasure{ get; set; }
        public string UnitType { get; set; }
        public float Weight { get; set; }
        public string MassUnitWeight { get; set; }

        public Guid? PictureID { get; set; }
        public Picture Picture { get; set; }

        public Stock Stock { get; private set; }

        public IList<string> OptionsMassUnit { get { return CtrlBoxUnits.CtrlBoxMassUnit; } }
        public IList<string> OptionsVolumeUnit { get { return CtrlBoxUnits.CtrlBoxVolumeUnit; } }

        public ICollection<ClientProductValue> CustomersProductsValues { get; set; }
        public ICollection<DeliveryDetail> DeliveriesDetails { get; set; }
        public ICollection<SaleProduct> SalesProducts { get; set; }
        public ICollection<BoxProductItems> BoxesProductsItems { get; set; }

        private Product()
            :base()
        {
            this.DeliveriesDetails = new HashSet<DeliveryDetail>();
            this.SalesProducts = new HashSet<SaleProduct>();
            this.CustomersProductsValues = new HashSet<ClientProductValue>();
            this.BoxesProductsItems = new HashSet<BoxProductItems>();

        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
                base.ComponentValidator.Validate(this, new ProductValidator());
            }
        }

        public void UpdateData(Product update)
        {
            this.Name = update.Name;
            this.Description = update.Description;
            this.Weight = update.Weight;
            this.DateModified = DateTime.Now;
            this.UnitMeasure = update.UnitMeasure;
        }
    }
}
