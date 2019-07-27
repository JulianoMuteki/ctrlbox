using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Repository
{
    public interface IBoxTrackingRepository : IGenericRepository<BoxTracking>
    {
        ICollection<BoxTracking> GetByBoxIDWithTrackingType(Guid boxID);
        ICollection<TrackingType> GetTrackingsTypesWithPictures();
    }
}
