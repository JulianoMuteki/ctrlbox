﻿using CtrlBox.CrossCutting;
using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Common;
using CtrlBox.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrlBox.Domain.Entities
{
    public class Box : EntityBase
    {
        public string Description { get; set; }
        public EBoxStatus Status { get; set; }
        public EFlowStep EFlowStep { get; set; }

        public int PorcentFull { get; set; }

        public Guid BoxTypeID { get; set; }
        public BoxType BoxType { get; set; }

        public Guid? BoxParentID { get; set; }
        public Box BoxParent { get; set; }

        public Guid? ProductID { get; set; }
        public Product Product { get; set; }

        public BoxBarcode BoxBarcode { get; set; }

        public ICollection<Box> BoxesChildren { get; set; }
        public ICollection<BoxProductItem> BoxesProductItems { get; set; }
        public ICollection<OrderBox> OrdersBoxes { get; set; }
        public ICollection<DeliveryBox> DeliveriesBoxes { get; set; }
        public ICollection<Tracking> Trackings { get; set; }

        internal Box()
            : base()
        {
            this.DeliveriesBoxes = new HashSet<DeliveryBox>();
            this.Trackings = new HashSet<Tracking>();
            this.BoxesChildren = new HashSet<Box>();
            this.BoxesProductItems = new HashSet<BoxProductItem>();
            this.OrdersBoxes = new HashSet<OrderBox>();
        }

        public static Box FactoryCreate(Guid boxTypeID, BoxType boxType, int i)
        {
            Box box = new Box();
            box.InicializateProperties();
            box.EFlowStep = EFlowStep.InStock;
            box.BoxTypeID = boxTypeID;
            box.Description = $"Box nº: {i} - {boxType.Name}";
            box.BoxType = boxType;

            if (!box.ComponentValidator.Validate(box, new BoxValidator()))
            {
                throw new CustomException(string.Join(", ", box.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
            }
            return box;
        }

        public void Destructor()
        {
            this.BoxType = null;
            this.Trackings = new HashSet<Tracking>();
            this.BoxesChildren = new HashSet<Box>();
            this.BoxesProductItems = new HashSet<BoxProductItem>();
            this.OrdersBoxes = new HashSet<OrderBox>();
        }

        public void SetBoxType(BoxType boxType)
        {
            this.BoxType = boxType;
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
                InicializateProperties();
            }
        }

        public void InicializateProperties()
        {
            this.BoxBarcode = new BoxBarcode();
            this.BoxBarcode.BoxID = this.Id;
            this.EFlowStep = EFlowStep.Create;
        }

        public void LoadProductItems(ICollection<ProductItem> productItems)
        {
            if (productItems.Count > this.BoxType.MaxProductsItems)
                throw CustomException.Create<Box>("Unexpected error MaxProductsItems ", nameof(this.LoadProductItems));

            foreach (var item in productItems)
            {
                BoxProductItem boxProductItem = new BoxProductItem
                {
                    BoxID = this.Id,
                    ProductItemID = item.Id
                };
                item.PutInTheBox();

                this.BoxesProductItems.Add(boxProductItem);
            }

            LoadFullBoxCompletedProductItems();
        }

        public int CountQuantityProductItems { get; set; }

        public void DoDelivery(DeliveryDetail deliveryDetail, int quantity)
        {
            deliveryDetail.AddDeliveryBox(this.Id);
            if (this.ProductID != Guid.Empty && this.BoxesProductItems.Count > 0)
            {
                var boxProductsItems = this.BoxesProductItems.Where(x => x.ProductItem.ProductID == deliveryDetail.ProductID && x.IsDelivered == false).Take(quantity).ToList();

                foreach (var boxProductItem in boxProductsItems)
                {
                    boxProductItem.Deliver();
                }

                this.BoxParent.CountQuantityProductItems -= boxProductsItems.Count;
                LoadFullBoxCompletedProductItems();
            }
            else
            {
                this.CountQuantityProductItems = quantity;

                foreach (var boxChild in this.BoxesChildren)
                {
                    if (this.CountQuantityProductItems == 0)
                        break;

                    boxChild.DoDelivery(deliveryDetail, this.CountQuantityProductItems);
                }

                if (this.BoxParentID != null && this.BoxParentID != Guid.Empty)
                    this.BoxParent.CountQuantityProductItems = this.CountQuantityProductItems;

                LoadFullBoxCompletedChildrem();
            }

            SetFlowDelivered();
        }

        private void SetFlowDelivered()
        {
            this.EFlowStep = EFlowStep.Delivery;
        }

        private void LoadFullBoxCompletedProductItems()
        {
            this.PorcentFull = (int)Math.Round((double)(100 * this.BoxesProductItems.Where(x => x.IsDelivered == false).ToList().Count) / this.BoxType.MaxProductsItems);
            SetBoxStatus();
        }

        private void SetBoxStatus()
        {
            if (this.BoxesProductItems.Count > 0 && this.PorcentFull < 100)
                this.Status = EBoxStatus.Loading;
            else if (this.PorcentFull == 100)
                this.Status = EBoxStatus.Full;
        }

        private void LoadFullBoxCompletedChildrem()
        {
            this.PorcentFull = (int)Math.Round((double)(100 * this.BoxesChildren.Count) / this.BoxType.MaxProductsItems);
            this.DateModified = DateTime.Now;

            if (this.BoxesChildren.Count > 0 && this.PorcentFull < 100)
                this.Status = EBoxStatus.Loading;
            else if (this.PorcentFull == 100)
                this.Status = EBoxStatus.Full;
        }

        public List<Box> AddChildren(List<Box> boxesChildren)
        {
            this.BoxesChildren = boxesChildren.Select(x => { x.BoxParentID = this.Id; return x; }).ToList();
            LoadFullBoxCompletedChildrem();

            return this.BoxesChildren.ToList();
        }

        public void AddTrackingProductItems(Guid trackingTypeID, Guid clientID)
        {
            if (this.ProductID != Guid.Empty && this.BoxesProductItems.Count > 0)
            {
                var boxProductsItems = this.BoxesProductItems.Where(x => x.ProductItem.EFlowStep == EFlowStep.Delivery).ToList();

                foreach (var boxProductItem in boxProductsItems)
                {
                    boxProductItem.AddTrackingProductItem(trackingTypeID, clientID);
                }
            }
            else
            {
                foreach (var boxChild in this.BoxesChildren)
                {
                    boxChild.AddTrackingProductItems(trackingTypeID, clientID);
                }
            }
        }

        public void AddTracking(Guid trackingTypeID, Guid clientID)
        {
            Tracking tracking = new Tracking()
            {
                TrackingTypeID = trackingTypeID,
                BoxID = this.Id
            };

            if (clientID != null && clientID != Guid.Empty)
            {
                tracking.TrackingsClients.Add(new TrackingClient()
                {
                    ClientID = clientID,
                    TrackingID = tracking.Id
                });
            }

            this.Trackings.Add(tracking);
        }

    }
}
