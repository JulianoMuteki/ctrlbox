
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Domain.Entities;

namespace CtrlBox.Application
{
    public class ClientApplicationService : IClientApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ClientVM Add(ClientVM entity)
        {
            var client = _mapper.Map<Client>(entity);

            _unitOfWork.Repository<Client>().Add(client);
            _unitOfWork.Commit();

            return entity;
        }

        public ICollection<ClientVM> GetAvailable(Guid routeID)
        {
            var clients = _unitOfWork.RepositoryCustom<IClientRepository>().GetAvailable(routeID);
            var clientsVMs = _mapper.Map<IList<ClientVM>>(clients);
            return clientsVMs;
        }

        public Task<ClientVM> AddAsync(ClientVM entity)
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

        public ICollection<ClientVM> GetAll()
        {
            var clients = _unitOfWork.Repository<Client>().GetAll();

            var clientsVMs = _mapper.Map<IList<ClientVM>>(clients);
            return clientsVMs;
        }

        public Task<ICollection<ClientVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ClientVM GetById(Guid id)
        {
            var client = _unitOfWork.Repository<Client>().GetById(id);

            var clientVM = _mapper.Map<ClientVM>(client);
            return clientVM;
        }

        public Task<ClientVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ClientVM Update(ClientVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<ClientVM> UpdateAsync(ClientVM updated)
        {
            throw new NotImplementedException();
        }

        public ICollection<ClientVM> GetNotAvailable(Guid idRoute)
        {
            var clients = _unitOfWork.RepositoryCustom<IClientRepository>().GetNotAvailable(idRoute);

            var clientsVMs = _mapper.Map<IList<ClientVM>>(clients);
            return clientsVMs;
        }

        public ICollection<ClientVM> GetByRouteID(Guid routeID)
        {
            var clients = _unitOfWork.RepositoryCustom<IClientRepository>().FindAll(x => x.RoutesClients.Where(r => r.RouteID == routeID).Select(c => c.ClientID).Contains(x.Id));
            IList<ClientVM> clientsVM = _mapper.Map<List<ClientVM>>(clients);

            return clientsVM;
        }
    }
}
