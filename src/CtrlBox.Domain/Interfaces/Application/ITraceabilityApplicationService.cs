using CtrlBox.Application.ViewModel;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface ITraceabilityApplicationService : IApplicationServiceBase<TraceabilityVM>
    {
        void AddTraceType(TraceTypeVM entity);
        ICollection<TraceTypeVM> GetAllTracesTypes();
    }
}
