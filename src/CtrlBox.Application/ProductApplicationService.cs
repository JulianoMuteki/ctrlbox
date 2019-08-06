using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.CrossCutting.Enums;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Domain.Validations;
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
                if (entity.Picture != null)
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
                    ProductItem productItem = new ProductItem();
                    productItem.Barcode = $"{i}{ DateTime.Now.Date.ToString("yyyyMMddHHmmss")}".Substring(0, 14);
                    productItem.ProductID = productID;
                    productItem.EFlowStep = EFlowStep.Available;
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
                var productItems = _unitOfWork.RepositoryCustom<IProductRepository>().GetAvailableProductItemByProductID(productID, quantity);

                IList<Tracking> trackings = new List<Tracking>();

                foreach (var productItem in productItems)
                {
                    productItem.EFlowStep = EFlowStep.InStock;

                    Tracking tracking = new Tracking()
                    {
                        TrackingTypeID = trackingTypeID,
                        ProductItemID = productItem.Id
                    };

                    tracking.TrackingsClients.Add(new TrackingClient()
                    {
                        ClientID = clientID,
                        TrackingID = tracking.Id
                    });
                    trackings.Add(tracking);
                }

                _unitOfWork.Repository<Tracking>().AddRange(trackings);
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
                    Box box = new Box()
                    {
                        EFlowStep = EFlowStep.InStock,
                        BoxTypeID = boxTypeID,
                        ProductID = productID,
                        Description = $"Box nº: {i} - {boxType.Name}",
                        BoxType = boxType,

                    };
                    box.Init();
                    var updateList = productsItems.Where(x => !productsItemsUpdate.Any(p => p.Id == x.Id)).Take(boxType.MaxProductsItems).ToList();
                    box.LoadProductItems(updateList);
                    productsItemsUpdate.AddRange(updateList);

                    if (!box.ComponentValidator.Validate(box, new BoxValidator()))
                    {
                        throw new CustomException(string.Join(", ", box.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
                    }

                    Tracking tracking = new Tracking()
                    {
                        TrackingTypeID = trackingTypeID,
                        BoxID = box.Id
                    };

                    tracking.TrackingsClients.Add(new TrackingClient()
                    {
                        ClientID = clientID,
                        TrackingID = tracking.Id
                    });

                    box.Trackings.Add(tracking);
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
                    Box box = new Box()
                    {
                        EFlowStep = EFlowStep.InStock,
                        BoxTypeID = boxTypeID,
                        Description = $"Box nº: {i} - {boxType.Name}",
                        BoxType = boxType,
                    };

                    var updateList = boxesChildrem.Where(x => !boxesUpdateChildrem.Any(p => p.Id == x.Id)).Take(boxType.MaxProductsItems).ToList();
                    box.AddChildren(updateList);
                    boxesUpdateChildrem.AddRange(updateList);

                    if (!box.ComponentValidator.Validate(box, new BoxValidator()))
                    {
                        throw new CustomException(string.Join(", ", box.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
                    }

                    Tracking tracking = new Tracking()
                    {
                        TrackingTypeID = trackingTypeID,
                        BoxID = box.Id
                    };

                    tracking.TrackingsClients.Add(new TrackingClient()
                    {
                        ClientID = clientID,
                        TrackingID = tracking.Id
                    });

                    box.Trackings.Add(tracking);
                    box.BoxType = null;
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
    }
}
