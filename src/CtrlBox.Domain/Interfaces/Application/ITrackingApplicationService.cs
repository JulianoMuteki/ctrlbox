using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface ITrackingApplicationService : IApplicationServiceBase<TrackingVM>
    {
        void AddTraceType(TrackingTypeVM entity);
        ICollection<TrackingTypeVM> GetAllTrackingsTypes();
        ICollection<TrackingVM> GetByBoxID(Guid boxID);
        TrackingTypeVM GetTrackTypeById(Guid guid);
        ICollection<TrackingTypeVM> GetAllTrackingsTypesByPlace();
    }
}
