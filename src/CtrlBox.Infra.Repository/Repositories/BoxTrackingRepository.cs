using System;
using System.Collections.Generic;
using System.Linq;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace CtrlBox.Infra.Repository.Repositories
{
    public class BoxTrackingRepository : GenericRepository<BoxTracking>, IBoxTrackingRepository
    {
        public BoxTrackingRepository(CtrlBoxContext context)
            : base(context)
        {

        }

        public ICollection<BoxTracking> GetByBoxIDWithTrackingType(Guid boxID)
        {
            try
            {
                return _context.Set<BoxTracking>()
                    .Include(x => x.TrackingType).ThenInclude(x => x.Picture)
                    .Include(x => x.BoxesTrackingClients).ThenInclude(c => c.Client)
                    .Where(x => x.BoxID != null && x.BoxID.Value == boxID)
                    .OrderBy(x => x.CreationDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxTrackingRepository>("Unexpected error fetching get Traceability", nameof(this.GetByBoxIDWithTrackingType), ex);
            }
        }

        public ICollection<TrackingType> GetTrackingsTypesWithPictures()
        {
            try
            {
                return _context.Set<TrackingType>()
                    .Include(x => x.Picture)
                    .OrderBy(x => x.CreationDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxTrackingRepository>("Unexpected error fetching get TraceType", nameof(this.GetTrackingsTypesWithPictures), ex);
            }
        }
    }
}
