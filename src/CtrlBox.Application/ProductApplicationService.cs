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

        public async Task<IEnumerable<Product>> GetAllClient()
        {
            return await _unitOfWork.Repository<Product>().GetAllAsync();

        }

        public ICollection<Product> GetAll()
        {
            return _unitOfWork.Repository<Product>().GetAll();

        }
    }
}
