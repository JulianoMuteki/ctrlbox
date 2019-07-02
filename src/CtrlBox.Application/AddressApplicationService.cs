using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;

namespace CtrlBox.Application
{
    public class AddressApplicationService : IAddressApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public AddressVM Add(AddressVM entity)
        {
            try
            {
                var address = _mapper.Map<Address>(entity);
                _unitOfWork.Repository<Address>().Add(address);
                _unitOfWork.Commit();

                return _mapper.Map<AddressVM>(address); ;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<AddressApplicationService>("Unexpected error fetching add sale", nameof(this.Add), ex);
            }
        }

        public Task<AddressVM> AddAsync(AddressVM entity)
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

        public ICollection<AddressVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<AddressVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public AddressVM GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AddressVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public AddressVM Update(AddressVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<AddressVM> UpdateAsync(AddressVM updated)
        {
            throw new NotImplementedException();
        }
    }
}
