using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;

namespace CtrlBox.Infra.Repository.Repositories
{
   public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(CtrlBoxContext context)
            : base(context)
        {

        }
    }
}
