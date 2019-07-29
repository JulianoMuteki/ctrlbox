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
using CtrlBox.CrossCutting;

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
            try
            {
                var client = _mapper.Map<Client>(entity);
                client.SetCategories(entity.ClientsCategoriesID);
                _unitOfWork.Repository<Client>().Add(client);
               _unitOfWork.CommitSync();

                return entity;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching all Add client", nameof(this.Add), ex);
            }
        }

        public ICollection<ClientVM> GetAvailable(Guid routeID)
        {
            try
            {
                var clients = _unitOfWork.RepositoryCustom<IClientRepository>().GetAvailable(routeID);
                var clientsVMs = _mapper.Map<IList<ClientVM>>(clients);
                return clientsVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching all available clients", nameof(this.GetAvailable), ex);
            }
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
            try
            {
                var clients = _unitOfWork.Repository<Client>().GetAll();

                var clientsVMs = _mapper.Map<IList<ClientVM>>(clients);
                return clientsVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching get client", nameof(this.GetAll), ex);
            }
        }

        public Task<ICollection<ClientVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ClientVM GetById(Guid id)
        {
            try
            {
                var client = _unitOfWork.RepositoryCustom<IClientRepository>().GetByIDWithCategories(id);

                var clientVM = _mapper.Map<ClientVM>(client);
                return clientVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching get client", nameof(this.GetById), ex);
            }
        }

        public Task<ClientVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ClientVM Update(ClientVM updated)
        {
            try
            {
                var client = _mapper.Map<Client>(updated);
                client.SetCategories(updated.ClientsCategoriesID);
                var clientsCategories = _unitOfWork.Repository<ClientCategory>().FindAll(x => x.ClientID == client.Id);
                var clientsCategoriesRemove = clientsCategories.Where(x => !client.ClientsCategories.Any(c => c.CategoryID == x.CategoryID)).ToList();
                var clientsCategoriesAdd = client.ClientsCategories.Where(x => !clientsCategories.Any(c => c.CategoryID == x.CategoryID)).ToList();

                _unitOfWork.Repository<Client>().Update(client);
                _unitOfWork.Repository<ClientCategory>().AddRange(clientsCategoriesAdd);
                _unitOfWork.Repository<ClientCategory>().DeleteRange(clientsCategoriesRemove);
                _unitOfWork.CommitSync();

                return updated;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching Update client", nameof(this.Update), ex);
            }
        }

        public Task<ClientVM> UpdateAsync(ClientVM updated)
        {
            throw new NotImplementedException();
        }

        public ICollection<ClientVM> GetNotAvailable(Guid idRoute)
        {
            try
            {
                var clients = _unitOfWork.RepositoryCustom<IClientRepository>().GetNotAvailable(idRoute);

                var clientsVMs = _mapper.Map<IList<ClientVM>>(clients);
                return clientsVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching get client", nameof(this.GetNotAvailable), ex);
            }
        }

        public ICollection<ClientVM> GetByRouteID(Guid routeID)
        {
            try
            {
                var clients = _unitOfWork.RepositoryCustom<IClientRepository>().FindAll(x => x.RoutesClients.Where(r => r.RouteID == routeID).Select(c => c.ClientID).Contains(x.Id));
                IList<ClientVM> clientsVM = _mapper.Map<List<ClientVM>>(clients);

                return clientsVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching get client", nameof(this.GetByRouteID), ex);
            }
        }

        public void AddCategory(CategoryVM categoryVM)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryVM);

                _unitOfWork.Repository<Category>().Add(category);
                _unitOfWork.CommitSync();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching all Add Category", nameof(this.AddCategory), ex);
            }
        }

        public ICollection<CategoryVM> GetAllCategories()
        {
            try
            {
                var categories = _unitOfWork.Repository<Category>().GetAll();

                var categoriesVMs = _mapper.Map<IList<CategoryVM>>(categories);
                return categoriesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching get Categories", nameof(this.GetAllCategories), ex);
            }
        }
    }
}
