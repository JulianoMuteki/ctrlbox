using AutoMapper;
using CtrlBox.Application.ViewModel;
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
            var route = _mapper.Map<Route>(entity);

            _unitOfWork.Repository<Route>().Add(route);
            _unitOfWork.Commit();

            return entity;
        }

        public Task<RouteVM> AddAsync(RouteVM entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<RouteClientVM> ConnectRouteToClient(ICollection<RouteClientVM> routesClientsVM)
        {
            var routesClients = _mapper.Map<List<RouteClient>>(routesClientsVM);

            var routeClientReturn = _unitOfWork.Repository<RouteClient>().AddRange(routesClients);
            _unitOfWork.Commit();

            return routesClientsVM;
        }

        public ICollection<RouteClientVM> RemoveRouteFromClient(ICollection<RouteClientVM> routesClientsVM)
        {
            var routesClients = _mapper.Map<List<RouteClient>>(routesClientsVM);

            foreach (var item in routesClients)
            {
                _unitOfWork.Repository<RouteClient>().Delete(item);
            }
            _unitOfWork.Commit();

            return routesClientsVM;
        }

        public void Delete(Guid id)
        {
            var Route = _unitOfWork.Repository<Route>().GetById(id);

            if (Route != null)
            {

                _unitOfWork.Repository<Route>().Delete(Route);
                _unitOfWork.Commit();
            }
        }

        public Task<Guid> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<RouteVM> GetAll()
        {
            var routes = _unitOfWork.Repository<Route>().GetAll();

            var routesVMs = _mapper.Map<List<RouteVM>>(routes);
            return routesVMs;
        }

        public Task<ICollection<RouteVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public RouteVM GetById(Guid id)
        {
            var route = _unitOfWork.Repository<Route>().GetById(id);
            var routeVM = _mapper.Map<RouteVM>(route);
            return routeVM;
        }

        public Task<RouteVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public RouteVM Update(RouteVM updated)
        {
            var route = _mapper.Map<Route>(updated);
            Route routeUpdate = _unitOfWork.Repository<Route>().GetById(route.Id);

            if (routeUpdate != null)
            {
                routeUpdate.UpdateData(route);
                _unitOfWork.Repository<Route>().Update(routeUpdate);
                _unitOfWork.Commit();
            }
            return updated;
        }

        public Task<RouteVM> UpdateAsync(RouteVM updated)
        {
            throw new NotImplementedException();
        }
    }
}
