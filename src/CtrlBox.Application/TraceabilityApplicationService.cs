using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Domain.Interfaces.Repository;

namespace CtrlBox.Application
{
    public class TraceabilityApplicationService : ITraceabilityApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TraceabilityApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BoxTrackingVM Add(BoxTrackingVM entity)
        {
            try
            {
                var traceability = _mapper.Map<BoxTracking>(entity);

                if(entity.ClientID != null && entity.ClientID != Guid.Empty)
                {
                    traceability.AddClient(entity.ClientID);
                }
                //if (!boxType.ComponentValidator.Validate(boxType, new BoxTypeValidator()))
                //{
                //    throw new CustomException(string.Join(", ", boxType.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
                //}

                _unitOfWork.Repository<BoxTracking>().Add(traceability);
               _unitOfWork.CommitSync();

                return entity;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<TraceabilityApplicationService>("Unexpected error fetching add trace", nameof(this.Add), ex);
            }
        }

        public Task<BoxTrackingVM> AddAsync(BoxTrackingVM entity)
        {
            throw new NotImplementedException();
        }

        public void AddTraceType(TraceTypeVM entity)
        {
            try
            {
                var traceType = _mapper.Map<TrackingType>(entity);

                //if (!boxType.ComponentValidator.Validate(boxType, new BoxTypeValidator()))
                //{
                //    throw new CustomException(string.Join(", ", boxType.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
                //}

                _unitOfWork.Repository<TrackingType>().Add(traceType);
               _unitOfWork.CommitSync();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<TraceabilityApplicationService>("Unexpected error fetching add trace type", nameof(this.AddTraceType), ex);
            }
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<BoxTrackingVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<BoxTrackingVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ICollection<TraceTypeVM> GetAllTracesTypes()
        {
            try
            {
                var tracesTypes = _unitOfWork.RepositoryCustom<ITraceabilityRepository>().GetTracesTypesWithPictures();
                var tracesTypesVMs = _mapper.Map<IList<TraceTypeVM>>(tracesTypes);
                return tracesTypesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<TraceabilityApplicationService>("Unexpected error fetching GetAllTracesTypes", nameof(this.GetAllTracesTypes), ex);
            }
        }

        public ICollection<BoxTrackingVM> GetByBoxID(Guid boxID)
        {
            try
            {
                var tracesTypes = _unitOfWork.RepositoryCustom<ITraceabilityRepository>().GetByBoxIDWithTraceType(boxID);
                var tracesTypesVMs = _mapper.Map<IList<BoxTrackingVM>>(tracesTypes);
                return tracesTypesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<TraceabilityApplicationService>("Unexpected error fetching GetAll TraceabilityVM", nameof(this.GetByBoxID), ex);
            }
        }

        public BoxTrackingVM GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BoxTrackingVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public BoxTrackingVM Update(BoxTrackingVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<BoxTrackingVM> UpdateAsync(BoxTrackingVM updated)
        {
            throw new NotImplementedException();
        }
    }
}
