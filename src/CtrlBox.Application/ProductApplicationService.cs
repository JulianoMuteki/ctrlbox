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

        public ProductApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ICollection<Product> GetAll()
        {
            return _unitOfWork.Repository<Product>().GetAll();
        }

        public Task<ICollection<Product>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Product GetById(Guid id)
        {
            return _unitOfWork.Repository<Product>().GetById(id);
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public Product Add(Product entity)
        {
            var product = _unitOfWork.Repository<Product>().Add(entity);
            _unitOfWork.Commit();

            return product;
        }

        public Task<Product> AddAsync(Product entity)
        {
            var product = _unitOfWork.Repository<Product>().AddAsync(entity);
            _unitOfWork.Commit();

            return product;
        }

        public Product Update(Product updated)
        {
            Product product = _unitOfWork.Repository<Product>().GetById(updated.Id);

            if (product != null)
            {
                product.UpdateData(updated);
                product = _unitOfWork.Repository<Product>().Update(product);
                _unitOfWork.Commit();
            }
            return product;
        }

        public Task<Product> UpdateAsync(Product updated)
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

        public ICollection<ClientProductValue> ConnectRouteToClient(ICollection<ClientProductValue> clientsProducts)
        {
            _unitOfWork.Repository<ClientProductValue>().AddRange(clientsProducts);
            _unitOfWork.Commit();

            return clientsProducts;
        }

        public int AddProductStock(ICollection<StockProduct> stocksProducts)
        {
            try
            {
                var stock = _unitOfWork.Repository<Stock>().GetAll().FirstOrDefault();

                var resultstocksProd = stocksProducts.Select(e =>
                {
                    e.StockID = stock.Id;
                    e.Product = null;
                    return e;
                }).ToList();

                var result = _unitOfWork.Repository<StockProduct>().AddRange(resultstocksProd);
                _unitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<StockProduct> GetProductsStock()
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

            return productsStock;
        }

        public ICollection<DeliveryProduct> GetDeliveryProducts(Guid deliveryID)
        {
            return _unitOfWork.RepositoryCustom<IDeliveryRepository>().GetDeliveryProductsLoad(deliveryID);
        }
    }
}
