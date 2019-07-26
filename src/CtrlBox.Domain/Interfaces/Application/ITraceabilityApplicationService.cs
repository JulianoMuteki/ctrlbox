using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface ITraceabilityApplicationService : IApplicationServiceBase<BoxTrackingVM>
    {
        void AddTraceType(TraceTypeVM entity);
        ICollection<TraceTypeVM> GetAllTracesTypes();
        ICollection<BoxTrackingVM> GetByBoxID(Guid boxID);

    }
}
