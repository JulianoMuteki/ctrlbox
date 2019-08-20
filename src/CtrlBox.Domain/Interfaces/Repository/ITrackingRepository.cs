using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Repository
{
    public interface ITrackingRepository : IGenericRepository<Tracking>
    {
        ICollection<Tracking> GetByBoxIDWithTrackingType(Guid boxID);
        ICollection<TrackingType> GetTrackingsTypesWithPictures();
    }
}
