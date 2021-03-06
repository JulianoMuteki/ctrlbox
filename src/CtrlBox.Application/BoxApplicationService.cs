﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Domain.Validations;

namespace CtrlBox.Application
{
    public class BoxApplicationService : IBoxApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BoxApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BoxVM Add(BoxVM entity)
        {
            try
            {
                var box = _mapper.Map<Box>(entity);
                var boxType = _unitOfWork.Repository<BoxType>().GetById(box.BoxTypeID);
                box.SetBoxType(boxType);

                //if (box.ProductID != null && box.ProductID != Guid.Empty)
                //    AddBoxHasProduct(entity.RangeProductsItems, box);
                //else
                AddBoxWithoutProduct(entity.ChildrenBoxesID, box);

                _unitOfWork.CommitSync();
                return entity;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching add product", nameof(this.Add), ex);
            }
        }

        private void AddBoxWithoutProduct(string[] boxesChildrenID, Box box)
        {
            var boxesChildren = _unitOfWork.Repository<Box>().FindAll(x => boxesChildrenID.Any(id => new Guid(id) == x.Id)).ToList();
            var boxesChildrenWithFather = box.AddChildren(boxesChildren);

            box.Destructor();
            _unitOfWork.Repository<Box>().Add(box);
            _unitOfWork.Repository<Box>().UpdateRange(boxesChildrenWithFather);
        }

        private void AddBoxHasProduct(int rangeProductsItems, Box box)
        {
            //var productItems = _unitOfWork.Repository<ProductItem>().FindAll(x => x.ProductID == box.ProductID && x.Status == EProductItemStatus.AvailableStock).OrderByDescending(x => x.CreationDate).Take(rangeProductsItems).ToList();
            //box.LoadProductItems(productItems);

            if (!box.ComponentValidator.Validate(box, new BoxValidator()))
            {
                throw new CustomException(string.Join(", ", box.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
            }
            // var list = box.BoxesProductItems.ToList();
            box.Destructor();

            _unitOfWork.Repository<Box>().Add(box);
            //_unitOfWork.Repository<BoxProductItem>().AddRange(list);
        }

        public Task<BoxVM> AddAsync(BoxVM entity)
        {
            throw new NotImplementedException();
        }

        public void AddBoxType(BoxTypeVM entity)
        {
            try
            {
                var boxType = _mapper.Map<BoxType>(entity);

                if (!boxType.ComponentValidator.Validate(boxType, new BoxTypeValidator()))
                {
                    throw new CustomException(string.Join(", ", boxType.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
                }

                _unitOfWork.Repository<BoxType>().Add(boxType);
                _unitOfWork.CommitSync();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching add product", nameof(this.Add), ex);
            }
        }

        public ICollection<BoxVM> GetBoxesStockParents(Guid routeID)
        {
            try
            {
                var route = _unitOfWork.Repository<Route>().GetById(routeID);
                var boxes = _unitOfWork.RepositoryCustom<IProductRepository>().GetBoxesInStockByClientID(route.ClientOriginID);
                var boxesVMs = _mapper.Map<IList<BoxVM>>(boxes);
                return boxesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes", nameof(this.GetAll), ex);
            }

        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<BoxVM> GetAll()
        {
            try
            {
                var boxes = _unitOfWork.RepositoryCustom<IBoxRepository>().GetAllWithBoxTypeAndProduct();
                var boxesVMs = _mapper.Map<IList<BoxVM>>(boxes);
                return boxesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes", nameof(this.GetAll), ex);
            }
        }

        public Task<ICollection<BoxVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ICollection<BoxTypeVM> GetAllBoxesType()
        {
            try
            {
                var boxesType = _unitOfWork.Repository<BoxType>().GetAll();
                var boxesTypeVMs = _mapper.Map<IList<BoxTypeVM>>(boxesType);
                return boxesTypeVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes type", nameof(this.GetAllBoxesType), ex);
            }
        }

        public BoxVM GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BoxVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public BoxVM Update(BoxVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<BoxVM> UpdateAsync(BoxVM updated)
        {
            throw new NotImplementedException();
        }

        public ICollection<BoxVM> GetBoxesByDeliveryID(Guid deliveryID)
        {
            try
            {
                var boxes = _unitOfWork.RepositoryCustom<IBoxRepository>().GetBoxesByDeliveryWithBoxType(deliveryID);
                var boxesVMs = _mapper.Map<IList<BoxVM>>(boxes);
                return boxesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes", nameof(this.GetBoxesByDeliveryID), ex);
            }
        }

        public ICollection<ProductItemVM> GetOrderProductItemByDeliveryID(Guid deliveryID)
        {
            try
            {
                var producstItems = _unitOfWork.RepositoryCustom<IBoxRepository>().GetOrderProductItemByDeliveryID(deliveryID);
                var productItemVMs = _mapper.Map<IList<ProductItemVM>>(producstItems);
                return productItemVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes", nameof(this.GetOrderProductItemByDeliveryID), ex);
            }
        }

        public ICollection<BoxVM> GetBoxesByBoxWithChildren(Guid boxID)
        {
            try
            {
                var boxes = _unitOfWork.RepositoryCustom<IBoxRepository>().GetBoxesByBoxWithChildren(boxID);
                var boxesVMs = _mapper.Map<IList<BoxVM>>(boxes);
                return boxesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes", nameof(this.GetBoxesByBoxWithChildren), ex);
            }
        }

        public BoxVM GetBoxesByIDWithBoxTypeAndProductItems(Guid boxID)
        {
            try
            {
                var box = _unitOfWork.RepositoryCustom<IBoxRepository>().GetBoxesByIDWithBoxTypeAndProductItems(boxID);
                var boxVM = _mapper.Map<BoxVM>(box);
                return boxVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes", nameof(this.GetBoxesByIDWithBoxTypeAndProductItems), ex);
            }
        }

        public IEnumerable<BoxVM> GetBoxesParentsWithBoxTypeEndProduct()
        {
            try
            {
                var boxes = _unitOfWork.RepositoryCustom<IBoxRepository>().GetBoxesParentsWithBoxTypeEndProduct();
                var boxesVMs = _mapper.Map<IList<BoxVM>>(boxes);
                return boxesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes", nameof(this.GetBoxesParentsWithBoxTypeEndProduct), ex);
            }
        }

        public ICollection<BoxVM> FindBoxesAvailableWithProducts()
        {
            try
            {
                var boxes = _unitOfWork.RepositoryCustom<IBoxRepository>().FindAll(x => x.BoxParentID == null);
                var boxesVMs = _mapper.Map<IList<BoxVM>>(boxes);
                return boxesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes", nameof(this.GetBoxesParentsWithBoxTypeEndProduct), ex);
            }
        }

        public ICollection<BoxVM> FindBoxesAvailableByBoxType(Guid guid)
        {
            try
            {
                var boxes = _unitOfWork.RepositoryCustom<IBoxRepository>().FindAll(x => x.BoxParentID == null && x.BoxTypeID == guid);
                var boxesVMs = _mapper.Map<IList<BoxVM>>(boxes);
                return boxesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes", nameof(this.FindBoxesAvailableByBoxType), ex);
            }
        }

        public void GenarateBoxes(int nivel)
        {
            if (nivel == 1)
            {
                try
                {
                    var boxType = _unitOfWork.Repository<BoxType>().GetById(new Guid("4FF493D4-118C-4925-A68C-BDC029D67F36"));
                    for (int c = 0; c < 210; c++)
                    {
                        BoxVM boxEngradado = new BoxVM()
                        {
                            BoxTypeID = boxType.Id,
                            Description = $"{c} - With 24 Coca-Cola",
                            Status = Enum.GetName(typeof(EBoxStatus), EBoxStatus.Empty),
                            ProductID = new Guid("45458722-5D7C-48F9-AE8D-96CDC4B31CE8"),
                            RangeProductsItems = 24
                        };

                        var box = _mapper.Map<Box>(boxEngradado);

                        box.SetBoxType(boxType);

                        //if (box.ProductID != null && box.ProductID != Guid.Empty)
                        //    AddBoxHasProduct(boxEngradado.RangeProductsItems, box);
                        //else
                        AddBoxWithoutProduct(boxEngradado.ChildrenBoxesID, box);

                        _unitOfWork.CommitSync();
                    }
                }
                catch (CustomException exc)
                {
                    throw exc;
                }
                catch (Exception ex)
                {
                    throw CustomException.Create<BoxApplicationService>("Unexpected error fetching add product", nameof(this.Add), ex);
                }
            }
            else if (nivel == 2)
            {
                try
                {
                    ICollection<BoxVM> boxesEngradados = FindBoxesAvailableWithProducts();
                    var boxType = _unitOfWork.Repository<BoxType>().GetById(new Guid("BDDE455A-7BC0-499F-A41E-E67399A46901"));

                    for (int p = 0; p < 5; p++)
                    {
                        BoxVM boxPallet = new BoxVM()
                        {
                            BoxTypeID = boxType.Id,
                            Description = $"{p} - With 42 engrado Coca-Cola",
                            Status = Enum.GetName(typeof(EBoxStatus), EBoxStatus.Empty),
                            BoxesChildren = boxesEngradados.Take(42).ToList()
                        };
                        boxPallet.ChildrenBoxesID = boxPallet.BoxesChildren.Select(x => x.DT_RowId).ToArray();

                        foreach (var item in boxPallet.BoxesChildren)
                        {
                            boxesEngradados.Remove(item);
                        }

                        var box = _mapper.Map<Box>(boxPallet);

                        box.SetBoxType(boxType);

                        //if (box.ProductID != null && box.ProductID != Guid.Empty)
                        //    AddBoxHasProduct(boxPallet.RangeProductsItems, box);
                        //else
                        AddBoxWithoutProduct(boxPallet.ChildrenBoxesID, box);

                        _unitOfWork.CommitSync();
                    }
                }
                catch (CustomException exc)
                {
                    throw exc;
                }
                catch (Exception ex)
                {
                    throw CustomException.Create<BoxApplicationService>("Unexpected error fetching add product", nameof(this.Add), ex);
                }
            }
            else
            {
                try
                {
                    ICollection<BoxVM> boxesPallets = FindBoxesAvailableByBoxType(new Guid("BDDE455A-7BC0-499F-A41E-E67399A46901"));
                    var boxType = _unitOfWork.Repository<BoxType>().GetById(new Guid("31C60A03-DF38-4391-9ECF-AA29D9529255"));


                    for (int i = 0; i < 1; i++)
                    {
                        BoxVM boxContainer = new BoxVM()
                        {
                            BoxTypeID = boxType.Id,
                            Description = $"{i} - With 5 pallets Coca-Cola",
                            Status = Enum.GetName(typeof(EBoxStatus), EBoxStatus.Empty),
                            BoxesChildren = boxesPallets.Take(5).ToList(),
                        };
                        boxContainer.ChildrenBoxesID = boxContainer.BoxesChildren.Select(x => x.DT_RowId).ToArray();

                        foreach (var item in boxContainer.BoxesChildren)
                        {
                            boxesPallets.Remove(item);
                        }

                        var box = _mapper.Map<Box>(boxContainer);

                        box.SetBoxType(boxType);

                        //if (box.ProductID != null && box.ProductID != Guid.Empty)
                        //    AddBoxHasProduct(boxContainer.RangeProductsItems, box);
                        //else
                        AddBoxWithoutProduct(boxContainer.ChildrenBoxesID, box);

                        _unitOfWork.CommitSync();
                    }
                }
                catch (CustomException exc)
                {
                    throw exc;
                }
                catch (Exception ex)
                {
                    throw CustomException.Create<BoxApplicationService>("Unexpected error fetching add product", nameof(this.Add), ex);
                }
            }

        }

        //Mobile
        public ICollection<string> GetListBarcodes(int quantity)
        {
            try
            {
                IList<GraphicCodes> listGraphicCodes = new List<GraphicCodes>(quantity);

                for (int i = 0; i < quantity; i++)
                {
                    GraphicCodes graphicCodes = GraphicCodes.FactoryCreate();
                    listGraphicCodes.Add(graphicCodes);
                }

                return listGraphicCodes.Select(gc => gc.BarcodeEAN13).ToList();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching Barcodes", nameof(this.GetListBarcodes), ex);
            }
        }

        public void Add(CreateBoxVM entity)
        {
            try
            {
                _unitOfWork.SetTrackAll();
                var boxtype = _unitOfWork.Repository<BoxType>().GetById(entity.BoxTypeID);

                int totalProductItemsToStock = 0;

                foreach (var barcode in entity.TagsBarcodes)
                {
                    Box box = _unitOfWork.Repository<Box>().Find(b => b.GraphicCodes.BarcodeEAN13 == barcode) ?? Box.FactoryCreate(boxtype, barcode);

                    if (entity.HasMovementStock)
                    {
                        int totalItemsByBox = SumTotalProductItemsForStock(entity, boxtype);
                        totalProductItemsToStock += totalItemsByBox;

                        box.StoreProductsItems(entity.ProductID, totalItemsByBox);
                    }
                    _unitOfWork.Repository<Box>().AddUpdate(box);
                }

                if (entity.HasMovementStock)
                {
                    AddStockAndMovement(entity, boxtype, totalProductItemsToStock);
                }
                _unitOfWork.CommitSync();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching add boxes", nameof(this.Add), ex);
            }
        }

        private int SumTotalProductItemsForStock(CreateBoxVM entity, BoxType boxtype)
        {
            var totalProductItems = entity.HasAutoCompleteItems ? boxtype.MaxProductsItems : entity.TotalItemsinBox;
            return totalProductItems;
        }

        private void AddStockAndMovement(CreateBoxVM entity, BoxType boxtype, int totalProductItems)
        {
            try
            {
                var stock = _unitOfWork.Repository<Stock>().Find(x => x.ProductID == entity.ProductID && x.ClientID == entity.ClientID);
                if (stock == null)
                {
                    stock = Stock.FactoryCreate(entity.ClientID, entity.ProductID);
                    stock.AddMovementInput(totalProductItems);
                    _unitOfWork.Repository<Stock>().Add(stock);
                }
                else
                {
                    stock.AddMovementInput(totalProductItems);
                    _unitOfWork.Repository<Stock>().Update(stock);
                }

            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching add boxes", nameof(this.Add), ex);
            }
        }
    }
}
