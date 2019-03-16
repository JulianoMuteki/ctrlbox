using CtrlBox.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IProductApplicationService
    {
        Task<IEnumerable<Product>> GetAllClient();
        ICollection<Product> GetAll();

    }
}
