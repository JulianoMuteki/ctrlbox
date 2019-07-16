﻿using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
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

        public ProductApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ICollection<ProductVM> GetAll()
        {
            try
            {
                var products = _unitOfWork.Repository<Product>().GetAll();
                var productsStocks = _unitOfWork.Repository<StockProduct>().GetAll();

                var productVMs = _mapper.Map<IList<ProductVM>>(products);
                var productsStocksVMs = _mapper.Map<IList<StockProductVM>>(productsStocks);

                foreach (var product in productVMs)
                {
                    product.StocksProducts = (from x in productsStocksVMs where x.ProductID.ToString() == product.DT_RowId select x).ToList();
                }

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

                _unitOfWork.Repository<Product>().Add(product);
                _unitOfWork.Commit();

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
                _unitOfWork.Commit();

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
                    _unitOfWork.Commit();
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
                    _unitOfWork.Commit();
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
                _unitOfWork.Commit();

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

        public int AddProductStock(ICollection<StockProductVM> stocksProductsVM)
        {
            try
            {
                var stProducts = _mapper.Map<IList<StockProduct>>(stocksProductsVM);
                var stocksProducts = _unitOfWork.Repository<StockProduct>().FindAll(x => stProducts.Any(s => s.ProductID == x.ProductID));

                foreach (var item in stocksProducts)
                {
                    item.Amount = (from x in stProducts where x.ProductID == item.ProductID select x.Amount).FirstOrDefault();
                }
                var stocksProductsAdd = stProducts.Where(x => !stocksProducts.Any(s => s.ProductID == x.ProductID)).ToList();

                _unitOfWork.Repository<StockProduct>().UpdateRange(stocksProducts);
                _unitOfWork.Repository<StockProduct>().AddRange(stocksProductsAdd);

                _unitOfWork.Commit();

                return 1;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching add product stock", nameof(this.AddProductStock), ex);
            }
        }

        public ICollection<StockProductVM> GetProductsStock()
        {
            try
            {
                var productsStock = _unitOfWork.Repository<StockProduct>().GetAll();

                var products = _unitOfWork.Repository<Product>().GetAll();
                var productsWithoutStock = products.Where(p => !productsStock.Any(sp => sp.ProductID == p.Id)).ToList();

                foreach (var product in productsWithoutStock)
                {
                    productsStock.Add(new StockProduct()
                    {
                        ProductID = product.Id,
                        Amount = 0,
                        Product = product
                    });
                }
                productsStock = productsStock.Select(p => { p.Product = (from x in products where x.Id == p.ProductID select x).FirstOrDefault(); return p; }).ToList();
                var stocksProductsVMs = _mapper.Map<IList<StockProductVM>>(productsStock);
                return stocksProductsVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching get product stock", nameof(this.GetProductsStock), ex);
            }
        }

        public ICollection<DeliveryProductVM> GetDeliveryProducts(Guid deliveryID)
        {
            try
            {
                var deliveries = _unitOfWork.RepositoryCustom<IDeliveryRepository>().GetDeliveryProductsLoad(deliveryID);

                var deliveriesVMs = _mapper.Map<IList<DeliveryProductVM>>(deliveries);
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

        public StockVM GetStock()
        {
            try
            {
                var stock = _unitOfWork.Repository<Stock>().GetAll().FirstOrDefault();

                var stockVM = _mapper.Map<StockVM>(stock);
                return stockVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ProductApplicationService>("Unexpected error fetching add stock", nameof(this.GetStock), ex);
            }
        }

        public void AddStock(int stockTotal)
        {
            try
            {
                Stock stock = new Stock();
                stock.AmountBoxes = stockTotal;

                _unitOfWork.Repository<Stock>().Add(stock);
                _unitOfWork.Commit();
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

        public void GenerateProductItem(Guid productID, int quantity)
        {
            try
            {
                IList<ProductItem> productsItems = new List<ProductItem>();

                for (int i = 0; i < quantity; i++)
                {
                    ProductItem productItem = new ProductItem();
                    productItem.Barcode = $"{i}{ DateTime.Now.Date.ToString("yyyyMMddHHmmss")}".Substring(0, 14);
                    productItem.Weight = "10";
                    productItem.ProductID = productID;
                    productsItems.Add(productItem);
                }

                _unitOfWork.Repository<ProductItem>().AddRange(productsItems);
                _unitOfWork.Commit();
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
    }
}
