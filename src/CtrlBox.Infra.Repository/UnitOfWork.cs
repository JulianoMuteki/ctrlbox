using CtrlBox.Infra.Repository.Repositories;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;

namespace CtrlBox.Infra.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CtrlBoxContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public Dictionary<Type, object> Repositories
        {
            get { return _repositories; }
            set { Repositories = value; }
        }

        public UnitOfWork(CtrlBoxContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)))
            {
                return Repositories[typeof(T)] as IGenericRepository<T>;
            }

            IGenericRepository<T> repo = new GenericRepository<T>(_dbContext);
            Repositories.Add(typeof(T), repo);
            return repo;
        }


        private readonly Dictionary<Type, object> _repositoriesCustom = new Dictionary<Type, object>();

        public Dictionary<Type, object> RepositoriesCustom
        {
            get { return _repositoriesCustom; }
            set { RepositoriesCustom = value; }
        }

        public T RepositoryCustom<T>() where T : class
        {
            if (!RepositoriesCustom.Keys.Contains(typeof(T)))
            {
                CreateReository<T>();
            }

            var obj = RepositoriesCustom[typeof(T)] as T;

            return obj;
        }

        private void CreateReository<T>() where T : class
        {
            if (typeof(IRouteRepository).Equals((typeof(T))) && !RepositoriesCustom.Keys.Contains(typeof(T)))
            {
                IRouteRepository repository = new RouteRepository(_dbContext);
                RepositoriesCustom.Add(typeof(T), repository);
            }
            else if (typeof(IClientRepository).Equals((typeof(T))) && !RepositoriesCustom.Keys.Contains(typeof(T)))
            {
                IClientRepository repository = new ClientRepository(_dbContext);
                RepositoriesCustom.Add(typeof(T), repository);
            }
            else if (typeof(IProductRepository).Equals((typeof(T))) && !RepositoriesCustom.Keys.Contains(typeof(T)))
            {
                IProductRepository repository = new ProductRepository(_dbContext);
                RepositoriesCustom.Add(typeof(T), repository);
            }
            else if (typeof(IDeliveryRepository).Equals((typeof(T))) && !RepositoriesCustom.Keys.Contains(typeof(T)))
            {
                IDeliveryRepository repository = new DeliveryRepository(_dbContext);
                RepositoriesCustom.Add(typeof(T), repository);
            }
            else if (typeof(ISaleRepository).Equals((typeof(T))) && !RepositoriesCustom.Keys.Contains(typeof(T)))
            {
                ISaleRepository repository = new SaleRepository(_dbContext);
                RepositoriesCustom.Add(typeof(T), repository);
            }
            else if (typeof(IBoxRepository).Equals((typeof(T))) && !RepositoriesCustom.Keys.Contains(typeof(T)))
            {
                IBoxRepository repository = new BoxRepository(_dbContext);
                RepositoriesCustom.Add(typeof(T), repository);
            }
        }

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}
