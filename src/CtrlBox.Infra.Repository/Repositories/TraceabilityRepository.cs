using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;

namespace CtrlBox.Infra.Repository.Repositories
{
    public class TraceabilityRepository : GenericRepository<Traceability>, ITraceabilityRepository
    {
        public TraceabilityRepository(CtrlBoxContext context)
            : base(context)
        {

        }
    }
}
