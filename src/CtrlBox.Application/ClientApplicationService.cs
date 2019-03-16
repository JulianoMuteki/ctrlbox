using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Client>> GetAllClient()
        {
            return await _unitOfWork.Repository<Client>().GetAll().AsQueryable().ToListAsync();

        }

        public ICollection<Client> GetAll()
        {
            return _unitOfWork.Repository<Client>().GetAll();

        }
    }
}
