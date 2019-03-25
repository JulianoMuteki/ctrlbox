using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
   public class RouteApplicationService: IRouteApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RouteApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Route Add(Route entity)
        {
            var Route = _unitOfWork.Repository<Route>().Add(entity);
            _unitOfWork.Commit();

            return Route;
        }

        public Task<Route> AddAsync(Route entity)
        {
            throw new NotImplementedException();
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

        public ICollection<Route> GetAll()
        {
            return _unitOfWork.Repository<Route>().GetAll();
        }

        public Task<ICollection<Route>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Route GetById(Guid id)
        {
            return _unitOfWork.Repository<Route>().GetById(id);
        }

        public Task<Route> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Route Update(Route updated)
        {
            Route Route = _unitOfWork.Repository<Route>().GetById(updated.Id);

            if (Route != null)
            {
                Route.UpdateData(updated);
                Route = _unitOfWork.Repository<Route>().Update(Route);
                _unitOfWork.Commit();
            }
            return Route;
        }

        public Task<Route> UpdateAsync(Route updated)
        {
            throw new NotImplementedException();
        }
    }
}
