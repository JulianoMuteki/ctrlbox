using CtrlBox.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IClientApplicationService
    {
        Task<IEnumerable<Client>> GetAllClient();
        ICollection<Client> GetAll();
    }
}
