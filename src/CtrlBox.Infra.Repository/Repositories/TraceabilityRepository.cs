﻿using System;
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
    public class TraceabilityRepository : GenericRepository<Traceability>, ITraceabilityRepository
    {
        public TraceabilityRepository(CtrlBoxContext context)
            : base(context)
        {

        }

        public ICollection<Traceability> GetByBoxIDWithTraceType(Guid boxID)
        {
            try
            {
                return _context.Set<Traceability>()
                    .Include(x => x.TraceType)
                    .Where(x => x.BoxID != null && x.BoxID.Value == boxID)
                    .OrderBy(x=>x.CreationDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<TraceabilityRepository>("Unexpected error fetching get Traceability", nameof(this.GetByBoxIDWithTraceType), ex);
            }
        }
    }
}
