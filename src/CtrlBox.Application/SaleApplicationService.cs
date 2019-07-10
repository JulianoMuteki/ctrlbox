using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    public class SaleApplicationService : ISaleApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaleApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public SaleVM Add(SaleVM entity)
        {
            try
            {
                var sale = _mapper.Map<Sale>(entity);

                var deliverysProducts = _unitOfWork.Repository<DeliveryProduct>().FindAll(x => x.DeliveryID == sale.DeliveryID);
                foreach (var item in sale.SalesProducts)
                {
                    var deliveryProduct = deliverysProducts.Where(x => x.ProductID == item.ProductID).FirstOrDefault();
                    deliveryProduct.SubtractProductsDelivered(item.Quantity);
                    item.CalcTotalValue();
                    _unitOfWork.Repository<DeliveryProduct>().Update(deliveryProduct);
                }

                sale.CalculatePayment();
                _unitOfWork.Repository<Sale>().Add(sale);
                _unitOfWork.Commit();

                return entity;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching add sale", nameof(this.Add), ex);
            }
        }

        public Task<SaleVM> AddAsync(SaleVM entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<SaleVM> FindAllByDelivery(Guid deliveryID)
        {
            try
            {
                var sales = _unitOfWork.Repository<Sale>().FindAll(x => x.DeliveryID == deliveryID);
                var salesVMs = _mapper.Map<IList<SaleVM>>(sales);
                return salesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching find sale", nameof(this.FindAllByDelivery), ex);
            }
        }

        public ICollection<SaleVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<SaleVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public SaleVM GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<SaleVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public SaleVM GetByClientIDAndDeliveryID(Guid clientID, Guid deliveryID)
        {
            try
            {
                var sale = _unitOfWork.Repository<Sale>().Find(x => x.ClientID == clientID && x.DeliveryID == deliveryID);
                if (sale != null)
                {
                    sale.SalesProducts = _unitOfWork.Repository<SaleProduct>().FindAll(x => x.SaleID == sale.Id);
                    sale.Payment = _unitOfWork.Repository<Payment>().Find(x => x.SaleID == sale.Id);
                    if (sale.Payment != null)
                        sale.Payment.PaymentsSchedules = _unitOfWork.Repository<PaymentSchedule>().FindAll(x => x.PaymentID == sale.Payment.Id);
                    var products = _unitOfWork.Repository<Product>().GetAll();

                    sale.SalesProducts = sale.SalesProducts.Select(c => { c.Product = (from x in products where x.Id == c.ProductID select x).FirstOrDefault(); return c; }).ToList();
                }

                var salesVMs = _mapper.Map<SaleVM>(sale);
                return salesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<SaleApplicationService>("Unexpected error fetching find sale", nameof(this.GetByClientIDAndDeliveryID), ex);
            }
        }

        public SaleVM Update(SaleVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<SaleVM> UpdateAsync(SaleVM updated)
        {
            throw new NotImplementedException();
        }

        public SaleVM GetInvoiceSaleByID(Guid saleID)
        {
            throw new NotImplementedException();
        }
    }
}
