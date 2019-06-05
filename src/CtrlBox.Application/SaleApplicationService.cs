using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    public class SaleApplicationService : ISaleApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Sale Add(Sale entity)
        {
            var deliverysProducts = _unitOfWork.Repository<DeliveryProduct>().FindAll(x => x.DeliveryID == entity.DeliveryID);
            foreach (var item in entity.SalesProducts)
            {
                var entregaProduto = deliverysProducts.Where(x => x.ProductID == item.ProductID).FirstOrDefault();
                entregaProduto.Amount -= (int)item.Amount;
                item.SaleID = entity.Id;
                
                _unitOfWork.Repository<DeliveryProduct>().Update(entregaProduto);
            }
            entity.IsFinished = true;

            _unitOfWork.Repository<Sale>().Add(entity);
            _unitOfWork.Commit();

            return entity;
        }

        public Task<Sale> AddAsync(Sale entity)
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

        public ICollection<Sale> FindAllByDelivery(Guid deliveryID)
        {
            var sales = _unitOfWork.Repository<Sale>().FindAll(x => x.DeliveryID == deliveryID);
            return sales;
        }

        public ICollection<Sale> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Sale>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Sale GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Sale> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Sale Update(Sale updated)
        {
            throw new NotImplementedException();
        }

        public Task<Sale> UpdateAsync(Sale updated)
        {
            throw new NotImplementedException();
        }
    }
}
