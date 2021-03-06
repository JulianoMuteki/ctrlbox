﻿using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly NotificationContext _notificationContext;

        public ProductApplicationService(IUnitOfWork unitOfWork, IMapper mapper, NotificationContext notificationContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationContext = notificationContext;
        }

        public ICollection<ProductVM> GetAll()
        {
            try
            {
                var products = _unitOfWork.Repository<Product>().GetAll();
                var productVMs = _mapper.Map<IList<ProductVM>>(products);
                return productVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching get product", nameof(this.GetAll), ex);
            }
        }

        public Task<ICollection<ProductVM>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public ProductVM GetById(Guid id)
        {
            try
            {
                var product = _unitOfWork.Repository<Product>().GetById(id);
                var productVM = _mapper.Map<ProductVM>(product);
                return productVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching get product", nameof(this.GetById), ex);
            }
        }

        public Task<ProductVM> GetByIdAsync(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public ProductVM Add(ProductVM entity)
        {
            try
            {
                var product = _mapper.Map<Product>(entity);
                if (entity.Picture != null)
                {
                    var picture = _mapper.Map<Picture>(entity.Picture);
                    _unitOfWork.Repository<Picture>().Add(picture);
                }
                _unitOfWork.Repository<Product>().Add(product);

                if (!product.ComponentValidator.IsValid)
                {
                    _notificationContext.AddNotifications(product.ComponentValidator.ValidationResult);
                    return entity;
                }

                _unitOfWork.CommitSync();

                return entity;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching add product", nameof(this.Add), ex);
            }
        }

        public Task<ProductVM> AddAsync(ProductVM entity)
        {
            try
            {
                var product = _mapper.Map<Product>(entity);

                _unitOfWork.Repository<Product>().AddAsync(product);
                _unitOfWork.CommitSync();

                return Task.FromResult(entity);
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching add delivery", nameof(this.AddAsync), ex);
            }
        }

        public ProductVM Update(ProductVM updated)
        {
            try
            {
                var product = _mapper.Map<Product>(updated);
                Product productUpdate = _unitOfWork.Repository<Product>().GetById(product.Id);

                if (productUpdate != null)
                {
                    productUpdate.UpdateData(product);
                    _unitOfWork.Repository<Product>().Update(productUpdate);
                    _unitOfWork.CommitSync();
                }
                return updated;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching update delivery", nameof(this.Update), ex);
            }
        }

        public Task<ProductVM> UpdateAsync(ProductVM updated)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Guid id)
        {
            try
            {
                var product = _unitOfWork.Repository<Product>().GetById(id);

                if (product != null)
                {

                    _unitOfWork.Repository<Product>().Delete(product);
                    _unitOfWork.CommitSync();
                }
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching delete delivery", nameof(this.Delete), ex);
            }
        }

        public Task<Guid> DeleteAsync(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<ClientProductValueVM> ConnectRouteToClient(ICollection<ClientProductValueVM> clientsProductsVMs)
        {
            try
            {
                var clientsProducts = _mapper.Map<IList<ClientProductValue>>(clientsProductsVMs);
                _unitOfWork.Repository<ClientProductValue>().AddRange(clientsProducts);
                _unitOfWork.CommitSync();

                return clientsProductsVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching connect Route to Client", nameof(this.ConnectRouteToClient), ex);
            }
        }

        public ICollection<DeliveryDetailVM> GetDeliveryProducts(Guid deliveryID)
        {
            try
            {
                var deliveries = _unitOfWork.RepositoryCustom<IDeliveryRepository>().GetDeliveryProductsLoad(deliveryID);

                var deliveriesVMs = _mapper.Map<IList<DeliveryDetailVM>>(deliveries);
                return deliveriesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching get delivery delivery", nameof(this.GetDeliveryProducts), ex);
            }
        }

        public ICollection<ClientProductValueVM> GetClientsProductsByClientID(Guid clientID)
        {
            try
            {
                var clientsProductsValue = _unitOfWork.Repository<ClientProductValue>().FindAll(x => x.ClientID == clientID);

                var clientsProductsValueVMs = _mapper.Map<IList<ClientProductValueVM>>(clientsProductsValue);
                return clientsProductsValueVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching add client product value", nameof(this.GetClientsProductsByClientID), ex);
            }
        }

        public void GenerateProductItem(Guid productID, int quantity)
        {
            try
            {
                IList<ProductItem> productsItems = new List<ProductItem>();

                for (int i = 0; i < quantity; i++)
                {
                    ProductItem productItem = ProductItem.FactoryCreate(productID);
                    productsItems.Add(productItem);
                }

                _unitOfWork.Repository<ProductItem>().AddRange(productsItems);
                _unitOfWork.CommitSync();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching add stock", nameof(this.GenerateProductItem), ex);
            }
        }

        public ICollection<ProductItemVM> GetProductsItems()
        {
            try
            {
                var productsItems = _unitOfWork.Repository<ProductItem>().GetAll();
                var products = _unitOfWork.Repository<Product>().GetAll();

                productsItems = productsItems.Select(x => { x.Product = (from p in products where p.Id == x.ProductID select p).FirstOrDefault(); return x; }).OrderBy(x => x.Barcode).ToList();

                var productsItemsVMs = _mapper.Map<IList<ProductItemVM>>(productsItems);
                return productsItemsVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching get product items", nameof(this.GetProductsItems), ex);
            }
        }

        public ICollection<ProductItemVM> GetProductsItemsAvailable(int quantity)
        {
            try
            {
                var productsItems = _unitOfWork.Repository<ProductItem>().FindAll(x => x.Status == EProductItemStatus.AvailableStock).Take(quantity);

                var productsItemsVMs = _mapper.Map<IList<ProductItemVM>>(productsItems);
                return productsItemsVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching get product items", nameof(this.GetProductsItemsAvailable), ex);
            }
        }

        public int GetTotalProductItemByProductID(Guid productID)
        {
            try
            {
                var total = _unitOfWork.RepositoryCustom<IProductRepository>().GetTotalProductItemByProductID(productID);

                return total;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching get product items", nameof(this.GetProductsItemsAvailable), ex);
            }
        }

        public void AddStockProduct(Guid productID, Guid clientID, Guid trackingTypeID, int quantity)
        {
            try
            {
                _unitOfWork.SetTrackAll();
                var productItems = _unitOfWork.RepositoryCustom<IProductRepository>().GetAvailableProductItemByProductID(productID, quantity);

                foreach (var productItem in productItems)
                {
                    productItem.AddInStock(trackingTypeID, clientID);
                }

                //  _unitOfWork.Repository<Tracking>().AddRange(trackings);
                _unitOfWork.Repository<ProductItem>().UpdateRange(productItems);
                _unitOfWork.CommitSync();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching Add Stock Product", nameof(this.AddStockProduct), ex);
            }
        }

        public ICollection<ProductItemVM> GetAvailableStockProductItemsByClientIDAndProductID(Guid productID, Guid clientID)
        {
            try
            {
                var productsItems = _unitOfWork.RepositoryCustom<IProductRepository>().GetAvailableStockProductItemsByClientIDAndProductID(productID, clientID);

                var productsItemsVMs = _mapper.Map<IList<ProductItemVM>>(productsItems);
                return productsItemsVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching get product items", nameof(this.GetProductsItemsAvailable), ex);
            }
        }

        public void AddBoxStockWithProductItems(Guid boxTypeID, Guid trackingTypeID, Guid clientID, Guid productID, int quantity)
        {
            try
            {
                var productsItems = _unitOfWork.RepositoryCustom<IProductRepository>().GetAvailableStockProductItemsByClientIDAndProductID(productID, clientID);
                var boxType = _unitOfWork.Repository<BoxType>().GetById(boxTypeID);
                List<ProductItem> productsItemsUpdate = new List<ProductItem>();
                IList<Box> boxes = new List<Box>();

                for (int i = 0; i < quantity; i++)
                {

                    var updateList = productsItems.Where(x => !productsItemsUpdate.Any(p => p.Id == x.Id)).Take(boxType.MaxProductsItems).ToList();
                    productsItemsUpdate.AddRange(updateList);

                    Box box = Box.FactoryCreate(boxTypeID, boxType, i, productID);
                    box.FlowStep.SetInStock();
                  //  box.LoadProductItems(updateList);
                    box.AddTracking(trackingTypeID, clientID);
                    box.BoxType = null;
                    boxes.Add(box);
                }

                _unitOfWork.Repository<Box>().AddRange(boxes);
                _unitOfWork.Repository<ProductItem>().UpdateRange(productsItemsUpdate);

                _unitOfWork.CommitSync();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching Add Stock Product", nameof(this.AddStockProduct), ex);
            }
        }

        public ICollection<BoxVM> GetAvailableBoxesByBoxTypeIDAndProductID(Guid boxTypeID, Guid clientID)
        {
            try
            {
                var boxes = _unitOfWork.RepositoryCustom<IProductRepository>().GetBoxesInStockByBoxTypeIDAndClientID(boxTypeID, clientID);

                var boxesVMs = _mapper.Map<IList<BoxVM>>(boxes);
                return boxesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching get product items", nameof(this.GetProductsItemsAvailable), ex);
            }
        }

        public void AddBoxStockWithBoxes(Guid boxTypeID, Guid trackingTypeID, Guid clientID, Guid boxTypeChildID, int quantity)
        {
            try
            {

                var boxesChildrem = _unitOfWork.RepositoryCustom<IProductRepository>().GetBoxesInStockByBoxTypeIDAndClientID(boxTypeChildID, clientID);

                var boxType = _unitOfWork.Repository<BoxType>().GetById(boxTypeID);
                List<Box> boxesUpdateChildrem = new List<Box>();
                IList<Box> boxes = new List<Box>();

                for (int i = 0; i < quantity; i++)
                {
                    var updateList = boxesChildrem.Where(x => !boxesUpdateChildrem.Any(p => p.Id == x.Id)).Take(boxType.MaxProductsItems).ToList();

                    Box box = Box.FactoryCreate(boxTypeID, boxType, i);
                    box.FlowStep.SetInStock();
                    boxesUpdateChildrem.AddRange(box.AddChildren(updateList));
                    box.AddTracking(trackingTypeID, clientID);
                    box.BoxType = null;
                    box.BoxesChildren = null;
                    boxes.Add(box);
                }

                _unitOfWork.Repository<Box>().AddRange(boxes);
                _unitOfWork.Repository<Box>().UpdateRange(boxesUpdateChildrem);

                _unitOfWork.CommitSync();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching Add Stock Product", nameof(this.AddStockProduct), ex);
            }
        }

        public void AddStock(StockVM stockVM)
        {
            try
            {
                var stock = _mapper.Map<Stock>(stockVM);

                _unitOfWork.Repository<Stock>().Add(stock);
                _unitOfWork.CommitSync();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching add stock", nameof(this.AddStock), ex);
            }
        }

        public ICollection<StockVM> GetStocks()
        {
            try
            {
                var stocks = _unitOfWork.RepositoryCustom<IProductRepository>().GetStocks();
                var stocksVM = _mapper.Map<IList<StockVM>>(stocks);
                return stocksVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching get stocks", nameof(this.GetStocks), ex);
            }
        }

        public ICollection<StockMovementVM> GetstocksMovements(Guid stockID)
        {
            try
            {
                var stocksMovements = _unitOfWork.RepositoryCustom<IProductRepository>().GetStocksMovements(stockID);
                var stocksMovementsVM = _mapper.Map<IList<StockMovementVM>>(stocksMovements);
                return stocksMovementsVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching get stocks", nameof(this.GetStocks), ex);
            }
        }

        public Guid AddStockMovement(StockMovementVM entityVM)
        {
            try
            {
                var stockMovement = _mapper.Map<StockMovement>(entityVM);
                _unitOfWork.SetTrackAll();

                var stock = _unitOfWork.Repository<Stock>().Find(x => x.ProductID == entityVM.ProductID);
                stockMovement.SetStock(stock);

                stock.StocksMovements.Add(stockMovement);
                _unitOfWork.Repository<Stock>().Update(stock);
                _unitOfWork.CommitSync();

                return stock.Id;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching add stock", nameof(this.AddStock), ex);
            }
        }
    }
}
