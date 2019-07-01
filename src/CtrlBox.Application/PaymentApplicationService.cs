using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    public class PaymentApplicationService : IPaymentApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PaymentVM Add(PaymentVM entity)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentVM> AddAsync(PaymentVM entity)
        {
            throw new NotImplementedException();
        }

        public void AddPaymentMethod(PaymentMethodVM paymentMethodVM)
        {
            try
            {
                var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodVM);

                _unitOfWork.Repository<PaymentMethod>().Add(paymentMethod);
                _unitOfWork.Commit();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<PaymentApplicationService>("Unexpected error fetching add PaymentMethod", nameof(this.AddPaymentMethod), ex);
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

        public ICollection<PaymentVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<PaymentVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public PaymentVM GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<PaymentMethodVM> GetPayMethods()
        {
            var payMethods = _unitOfWork.Repository<PaymentMethod>().GetAll();
            var payMethodsVMs = _mapper.Map<IList<PaymentMethodVM>>(payMethods);
            return payMethodsVMs;
        }

        public PaymentVM Update(PaymentVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentVM> UpdateAsync(PaymentVM updated)
        {
            throw new NotImplementedException();
        }
    }
}
