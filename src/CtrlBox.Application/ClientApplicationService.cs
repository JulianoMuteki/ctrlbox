using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    public class ClientApplicationService : IClientApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Client Add(Client entity)
        {
            var client = _unitOfWork.Repository<Client>().Add(entity);
            _unitOfWork.Commit();

            return client;
        }

        public ICollection<Client> GetAvailable(Guid routeID)
        {
            return _unitOfWork.RepositoryCustom<IClientRepository>().GetAvailable(routeID);
        }

        public Task<Client> AddAsync(Client entity)
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

        public ICollection<Client> GetAll()
        {
            return _unitOfWork.Repository<Client>().GetAll();
        }

        public Task<ICollection<Client>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Client GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Client Update(Client updated)
        {
            throw new NotImplementedException();
        }

        public Task<Client> UpdateAsync(Client updated)
        {
            throw new NotImplementedException();
        }

        public ICollection<Client> GetNotAvailable(Guid idRoute)
        {
            return _unitOfWork.RepositoryCustom<IClientRepository>().GetNotAvailable(idRoute);
        }
    }
}
