using AutoMapper;
using CtrlBox.Application.ViewModel;
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
