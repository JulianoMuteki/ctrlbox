using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System;
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

        public Product GetById(Guid id)
        {
            return _unitOfWork.Repository<Product>().GetById(id);
        }

        public Task<Product> GetByIdAsync(Guid id)
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
            Product product = _unitOfWork.Repository<Product>().GetById(updated.Id);

            if (product != null)
            {
                product.UpdateData(updated);
                product = _unitOfWork.Repository<Product>().Update(product);
               _unitOfWork.Commit();
            }
            return product;
        }

        public Task<Product> UpdateAsync(Product updated)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Guid id)
        {
            var product = _unitOfWork.Repository<Product>().GetById(id);

           if(product != null)
            {
               
                 _unitOfWork.Repository<Product>().Delete(product);
                _unitOfWork.Commit();
            }
        }

        public Task<Guid> DeleteAsync(Guid id)
        {
            throw new System.NotImplementedException();
        }
    }
}
