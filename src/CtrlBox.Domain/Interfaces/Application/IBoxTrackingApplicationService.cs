using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IBoxTrackingApplicationService : IApplicationServiceBase<BoxTrackingVM>
    {
        void AddTraceType(TrackingTypeVM entity);
        ICollection<TrackingTypeVM> GetAllTrackingsTypes();
        ICollection<BoxTrackingVM> GetByBoxID(Guid boxID);

    }
}
