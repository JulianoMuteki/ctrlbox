using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    public class RouteApplicationService : IRouteApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RouteApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public RouteVM Add(RouteVM entity)
        {
            try
            {
                var route = _mapper.Map<Route>(entity);

                _unitOfWork.Repository<Route>().Add(route);
               _unitOfWork.CommitSync();

                return entity;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching add route", nameof(this.Add), ex);
            }
        }

        public Task<RouteVM> AddAsync(RouteVM entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<RouteClientVM> ConnectRouteToClient(ICollection<RouteClientVM> routesClientsVM)
        {
            try
            {
                var routesClients = _mapper.Map<List<RouteClient>>(routesClientsVM);

                var routeClientReturn = _unitOfWork.Repository<RouteClient>().AddRange(routesClients);
               _unitOfWork.CommitSync();

                return routesClientsVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching connect Route to Client", nameof(this.ConnectRouteToClient), ex);
            }
        }

        public ICollection<RouteClientVM> RemoveRouteFromClient(ICollection<RouteClientVM> routesClientsVM)
        {
            try
            {
                var routesClients = _mapper.Map<List<RouteClient>>(routesClientsVM);

                foreach (var item in routesClients)
                {
                    _unitOfWork.Repository<RouteClient>().Delete(item);
                }
               _unitOfWork.CommitSync();

                return routesClientsVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching remove Route to Client", nameof(this.RemoveRouteFromClient), ex);
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                var Route = _unitOfWork.Repository<Route>().GetById(id);

                if (Route != null)
                {

                    _unitOfWork.Repository<Route>().Delete(Route);
                   _unitOfWork.CommitSync();
                }
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching delete route", nameof(this.Delete), ex);
            }
        }

        public Task<Guid> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<RouteVM> GetAll()
        {
            try
            {
                var routes = _unitOfWork.Repository<Route>().FindAll(x => x.HasOpenOrder == false);

                var routesVMs = _mapper.Map<List<RouteVM>>(routes);
                return routesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching connect get routes", nameof(this.GetAll), ex);
            }
        }

        public Task<ICollection<RouteVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public RouteVM GetById(Guid id)
        {
            try
            {
                var route = _unitOfWork.Repository<Route>().GetById(id);
                var routeVM = _mapper.Map<RouteVM>(route);
                return routeVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching connect get route", nameof(this.GetById), ex);
            }
        }

        public Task<RouteVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public RouteVM Update(RouteVM updated)
        {
            try
            {
                var route = _mapper.Map<Route>(updated);
                Route routeUpdate = _unitOfWork.Repository<Route>().GetById(route.Id);

                if (routeUpdate != null)
                {
                    routeUpdate.UpdateData(route);
                    _unitOfWork.Repository<Route>().Update(routeUpdate);
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
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching update route", nameof(this.Update), ex);
            }
        }

        public Task<RouteVM> UpdateAsync(RouteVM updated)
        {
            throw new NotImplementedException();
        }
    }
}
