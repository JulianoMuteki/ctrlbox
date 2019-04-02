using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    public class DeliveryApplicationService : IDeliveryApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeliveryApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Delivery Add(Delivery entity)
        {
            entity.Init();
            foreach (var item in entity.DeliveriesProducts)
            {
                item.DeliveryID = entity.Id;
                item.Amount = 33;

                var stockProduct = _unitOfWork.Repository<StockProduct>().Find(x => x.ProductID == item.ProductID);

                stockProduct.Amount -= item.Amount;
                _unitOfWork.Repository<StockProduct>().Update(stockProduct);
            }
            var delivery = _unitOfWork.Repository<Delivery>().Add(entity);

            _unitOfWork.Commit();

            return delivery;
        }

        public Task<Delivery> AddAsync(Delivery entity)
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

        public ICollection<Delivery> GetAll()
        {
            return _unitOfWork.Repository<Delivery>().GetAll();
        }

        public Task<ICollection<Delivery>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Delivery GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Delivery> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Delivery Update(Delivery updated)
        {
            throw new NotImplementedException();
        }

        public Task<Delivery> UpdateAsync(Delivery updated)
        {
            throw new NotImplementedException();
        }
    }
}
