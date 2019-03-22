using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ICollection<Product> GetAll()
        {
            return _unitOfWork.Repository<Product>().GetAll();
        }

        public Task<ICollection<Product>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Product GetByUniqueId(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> GetByUniqueIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Product Add(Product entity)
        {
            var product = _unitOfWork.Repository<Product>().Add(entity);
            _unitOfWork.Commit();

            return product;
        }

        public Task<Product> AddAsync(Product entity)
        {
            var product = _unitOfWork.Repository<Product>().AddAsync(entity);
            _unitOfWork.Commit();

            return product;
        }

        public Product Update(Product updated)
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product updated)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Product t)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(Product t)
        {
            throw new System.NotImplementedException();
        }
    }
}
