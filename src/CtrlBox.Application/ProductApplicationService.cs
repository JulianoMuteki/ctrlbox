using AutoMapper;
using CtrlBox.Application.ViewModel;
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

        public Task<ICollection<ProductVM>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public ProductVM GetById(Guid id)
        {
            var product = _unitOfWork.Repository<Product>().GetById(id);
            var productVM = _mapper.Map<ProductVM>(product);
            return productVM;
        }

        public Task<ProductVM> GetByIdAsync(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public ProductVM Add(ProductVM entity)
        {
            var product = _mapper.Map<Product>(entity);

            _unitOfWork.Repository<Product>().Add(product);
            _unitOfWork.Commit();

            return entity;
        }

        public Task<ProductVM> AddAsync(ProductVM entity)
        {
            var product = _mapper.Map<Product>(entity);

            _unitOfWork.Repository<Product>().AddAsync(product);
            _unitOfWork.Commit();

            return Task.FromResult(entity);
        }

        public ProductVM Update(ProductVM updated)
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

        public Task<ProductVM> UpdateAsync(ProductVM updated)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Guid id)
        {
            var product = _unitOfWork.Repository<Product>().GetById(id);

            if (product != null)
            {

                _unitOfWork.Repository<Product>().Delete(product);
                _unitOfWork.Commit();
            }
        }

        public Task<Guid> DeleteAsync(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<ClientProductValueVM> ConnectRouteToClient(ICollection<ClientProductValueVM> clientsProductsVMs)
        {
            var clientsProducts = _mapper.Map<IList<ClientProductValue>>(clientsProductsVMs);
            _unitOfWork.Repository<ClientProductValue>().AddRange(clientsProducts);
            _unitOfWork.Commit();

            return clientsProductsVMs;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<StockProductVM> GetProductsStock()
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

            var stocksProductsVMs = _mapper.Map<IList<StockProductVM>>(productsStock);
            return stocksProductsVMs;
        }

        public ICollection<DeliveryProductVM> GetDeliveryProducts(Guid deliveryID)
        {
            var deliveries = _unitOfWork.RepositoryCustom<IDeliveryRepository>().GetDeliveryProductsLoad(deliveryID);

            var deliveriesVMs = _mapper.Map<IList<DeliveryProductVM>>(deliveries);
            return deliveriesVMs;
        }

        public ICollection<ClientProductValueVM> GetClientsProductsByClientID(Guid clientID)
        {
            var clientsProductsValue = _unitOfWork.Repository<ClientProductValue>().FindAll(x => x.ClientID == clientID);

            var clientsProductsValueVMs = _mapper.Map<IList<ClientProductValueVM>>(clientsProductsValue);
            return clientsProductsValueVMs;
        }

        public StockVM GetStock()
        {
            var stock = _unitOfWork.Repository<Stock>().GetAll().FirstOrDefault();

            var stockVM = _mapper.Map<StockVM>(stock);
            return stockVM;
        }

        public void AddStock(int stockTotal)
        {
            Stock stock = new Stock();
            stock.AmountBoxes = stockTotal;

            _unitOfWork.Repository<Stock>().Add(stock);
            _unitOfWork.Commit();
        }
    }
}
