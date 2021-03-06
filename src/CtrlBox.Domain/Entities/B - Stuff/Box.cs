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
        public Guid BoxTypeID { get; set; }
        public Guid? BoxParentID { get; set; }
        public Guid GraphicCodeID { get; set; }

        public string Description { get; set; }
        public int PorcentFull { get; set; }
        public EBoxStatus Status { get; set; }
        public FlowStep FlowStep { get; set; }

        public BoxType BoxType { get; set; }
        public Box BoxParent { get; set; }
        public BoxProductItems BoxProductItems { get; set; }
        public GraphicCodes GraphicCodes { get; set; }

        public ICollection<Box> BoxesChildren { get; set; }
        public ICollection<TrackingBox> TrackingsBoxes { get; set; }

        private Box()
            : base()
        {
            this.DeliveriesBoxes = new HashSet<DeliveryBox>();
            this.TrackingsBoxes = new HashSet<TrackingBox>();
            this.BoxesChildren = new HashSet<Box>();
            this.OrdersBoxes = new HashSet<OrderBox>();
        }

        public static Box FactoryCreate(BoxType boxType, string barcode)
        {
            Box box = new Box();
            box.BoxTypeID = boxType.Id;
            box.BoxType = boxType;
            box.Description = $"Desc.{barcode} - {boxType.Name}";
            box.FlowStep = FlowStep.FactoryCreate();
            box.GraphicCodes = GraphicCodes.FactoryCreate(barcode);

            box.ComponentValidator.Validate(box, new BoxValidator());
            return box;
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }

        public void StoreProductsItems(Guid productID, int totalItems)
        {
            this.BoxProductItems = BoxProductItems.FactoryCreate(this.Id, productID, totalItems);
            LoadFullBoxCompletedProductItems();
        }

        private void LoadFullBoxCompletedProductItems()
        {
            this.PorcentFull = (int)Math.Round((double)(100 * this.BoxProductItems.TotalItems) / this.BoxType.MaxProductsItems);
            SetBoxStatus();
        }

        private void SetBoxStatus()
        {
            if (this.BoxProductItems.TotalItems > 0 && this.PorcentFull < 100)
                this.Status = EBoxStatus.Loading;
            else if (this.PorcentFull == 100)
                this.Status = EBoxStatus.Full;
        }


        #region OLD-REFACTORING

        public ICollection<OrderBox> OrdersBoxes { get; set; }
        public ICollection<DeliveryBox> DeliveriesBoxes { get; set; }

        public static Box FactoryCreate(Guid boxTypeID, BoxType boxType, int i, Guid? productID = null)
        {
            Box box = new Box();
            box.BoxTypeID = boxTypeID;
            box.BoxType = boxType;
            box.Description = $"Desc.{i} - {boxType.Name}";

            box.ComponentValidator.Validate(box, new BoxValidator());
            return box;
        }

        public void Destructor()
        {
            this.BoxType = null;
            this.TrackingsBoxes = new HashSet<TrackingBox>();
            this.BoxesChildren = new HashSet<Box>();
            this.OrdersBoxes = new HashSet<OrderBox>();
        }

        public void SetBoxType(BoxType boxType)
        {
            this.BoxType = boxType;
        }

        /*
        public void LoadProductItems(ICollection<ProductItem> productItems)
        {
            if (productItems.Count > this.BoxType.MaxProductsItems)
                throw CustomException.Create<Box>("Unexpected error MaxProductsItems ", nameof(this.LoadProductItems));

            foreach (var item in productItems)
            {
                BoxProductItem boxProductItem = BoxProductItem.FactoryCreate(this.Id, item.Id);
                item.PutInTheBox();

                this.BoxesProductItems.Add(boxProductItem);
            }

            LoadFullBoxCompletedProductItems();
        }
        */

        public int CountQuantityProductItems { get; set; }

        /*  public int DoDelivery(DeliveryDetail deliveryDetail, int quantity)
        public int DoDelivery(DeliveryDetail deliveryDetail, int quantity)
        {
            var totalProductItemsDelivered = 0;

            deliveryDetail.AddDeliveryBox(this.Id);
            // if (this.ProductID != Guid.Empty && this.BoxesProductItems.Where(x => x.IsItemRemovedBox == false).Count() > 0)
            if (this.BoxesProductItems.Where(x => x.IsItemRemovedBox == false).Count() > 0)
            {
                var boxProductsItems = this.BoxesProductItems.Where(x => x.IsItemRemovedBox == false && x.ProductItem.ProductID == deliveryDetail.ProductID).Take(quantity).ToList();

                foreach (var boxProductItem in boxProductsItems)
                {
                    boxProductItem.Deliver(deliveryDetail.HasCrossDocking);
                    totalProductItemsDelivered++;
                }

                SubtractProductItemsOfBoxParent(boxProductsItems);
                LoadFullBoxCompletedProductItems();

                if (deliveryDetail.HasCrossDocking)
                {
                    this.FlowStep.SetFlowCrossDocking();
                }
                else if (!this.BoxesProductItems.Any(x => x.ProductItem.FlowStep.EFlowStep != EFlowStep.Delivered) && !deliveryDetail.HasCrossDocking)
                {
                    this.FlowStep.SetFlowDelivered();
                }
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

            return totalProductItemsDelivered;
        }
        */

        private void SubtractProductItemsOfBoxParent(List<BoxProductItem> boxProductsItems)
        {
            if (this.BoxParent != null)
                this.BoxParent.CountQuantityProductItems -= boxProductsItems.Count;
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

        public void AddTracking(Guid trackingTypeID, Guid clientID)
        {
            ResetLastTrack();

            Tracking tracking = Tracking.FactoryCreate(trackingTypeID, this.Id);

            if (clientID != null && clientID != Guid.Empty)
            {
                tracking.TrackingsClients.Add(TrackingClient.FactoryCreate(tracking.Id, clientID));
            }

            //  this.TrackingsBoxes.Add(tracking);
        }

        private void ResetLastTrack()
        {
            //  this.TrackingsBoxes = this.TrackingsBoxes.Select(x => { x.IsLastTrack = false; return x; }).ToList();
        }

        public void AddTrackingProductItems(Guid trackingTypeID, Guid clientID)
        {
            //if (this.ProductID != Guid.Empty && this.BoxesProductItems.Count > 0)
            //{
            //    var boxProductsItems = this.BoxesProductItems.Where(x => x.ProductItem.FlowStep.EFlowStep == EFlowStep.Delivery).ToList();

            //    foreach (var boxProductItem in boxProductsItems)
            //    {
            //        boxProductItem.AddTrackingProductItem(trackingTypeID, clientID);
            //    }
            //}
            //else
            //{
            //    foreach (var boxChild in this.BoxesChildren)
            //    {
            //        boxChild.AddTrackingProductItems(trackingTypeID, clientID);
            //    }
            //}
        }

        public void FinishDelivery(bool hasCrossDocking)
        {
            this.FlowStep.SetFlowDelivery(hasCrossDocking);

            //if (this.ProductID != Guid.Empty && this.BoxesProductItems.Count > 0)
            //{
            //    var boxProductsItems = this.BoxesProductItems.Where(x => x.ProductItem.FlowStep.EFlowStep == EFlowStep.Delivery).ToList();

            //    foreach (var boxProductItem in boxProductsItems)
            //    {
            //        boxProductItem.FinishDelivery(hasCrossDocking);
            //    }
            //}
            //else
            //{
            //    foreach (var boxChild in this.BoxesChildren)
            //    {
            //        boxChild.FinishDelivery(hasCrossDocking);
            //    }
            //}
        }
        #endregion
    }
}
