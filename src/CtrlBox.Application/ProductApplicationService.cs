using AutoMapper;
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
                if(entity.Picture != null)
                {
                    var picture = _mapper.Map<Picture>(entity.Picture);
                    _unitOfWork.Repository<Picture>().Add(picture);
                }
                _unitOfWork.Repository<Product>().Add(product);
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

        public void GenerateProductItem(Guid productID, int quantity)
        {
            try
            {
                IList<ProductItem> productsItems = new List<ProductItem>();

                for (int i = 0; i < quantity; i++)
                {
                    ProductItem productItem = new ProductItem();
                    productItem.Barcode = $"{i}{ DateTime.Now.Date.ToString("yyyyMMddHHmmss")}".Substring(0, 14);
                    productItem.ProductID = productID;
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

                productsItems = productsItems.Select(x => { x.Product = (from p in products where p.Id == x.ProductID select p).FirstOrDefault(); return x; }).OrderBy(x=>x.Barcode).ToList();

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
    }
}
