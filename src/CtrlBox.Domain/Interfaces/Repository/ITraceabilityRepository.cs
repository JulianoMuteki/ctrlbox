﻿using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Repository
{
    public interface ITraceabilityRepository: IGenericRepository<Traceability>
    {
        ICollection<Traceability> GetByBoxIDWithTraceType(Guid boxID);
        ICollection<TraceType> GetTracesTypesWithPictures();
    }
}