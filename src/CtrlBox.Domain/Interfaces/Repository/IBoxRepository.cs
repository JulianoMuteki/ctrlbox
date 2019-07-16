using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Repository
{
    public interface IBoxRepository : IGenericRepository<Box>
    {
        ICollection<Box> GetAllWithBoxTypeAndProduct();
    }
}
