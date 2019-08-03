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
    public class TrackingApplicationService : ITrackingApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrackingApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public TrackingVM Add(TrackingVM entity)
        {
            try
            {
                var traceability = _mapper.Map<Tracking>(entity);

                if(entity.ClientID != null && entity.ClientID != Guid.Empty)
                {
                    traceability.AddClient(entity.ClientID);
                }
                //if (!boxType.ComponentValidator.Validate(boxType, new BoxTypeValidator()))
                //{
                //    throw new CustomException(string.Join(", ", boxType.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
                //}

                _unitOfWork.Repository<Tracking>().Add(traceability);
               _unitOfWork.CommitSync();

                return entity;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<TrackingApplicationService>("Unexpected error fetching add trace", nameof(this.Add), ex);
            }
        }

        public Task<TrackingVM> AddAsync(TrackingVM entity)
        {
            throw new NotImplementedException();
        }

        public void AddTraceType(TrackingTypeVM entity)
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
                throw CustomException.Create<TrackingApplicationService>("Unexpected error fetching add trace type", nameof(this.AddTraceType), ex);
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

        public ICollection<TrackingVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TrackingVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ICollection<TrackingTypeVM> GetAllTrackingsTypes()
        {
            try
            {
                var tracesTypes = _unitOfWork.RepositoryCustom<ITrackingRepository>().GetTrackingsTypesWithPictures();
                var tracesTypesVMs = _mapper.Map<IList<TrackingTypeVM>>(tracesTypes);
                return tracesTypesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<TrackingApplicationService>("Unexpected error fetching GetAllTracesTypes", nameof(this.GetAllTrackingsTypes), ex);
            }
        }

        public ICollection<TrackingTypeVM> GetAllTrackingsTypesByPlace()
        {
            try
            {
                var tracesTypes = _unitOfWork.Repository<TrackingType>().FindAll(x => x.TrackType == CrossCutting.Enums.ETrackType.Place);
                var tracesTypesVMs = _mapper.Map<IList<TrackingTypeVM>>(tracesTypes);
                return tracesTypesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<TrackingApplicationService>("Unexpected error fetching GetAllTracesTypes", nameof(this.GetAllTrackingsTypes), ex);
            }
        }

        public ICollection<TrackingVM> GetByBoxID(Guid boxID)
        {
            try
            {
                var tracesTypes = _unitOfWork.RepositoryCustom<ITrackingRepository>().GetByBoxIDWithTrackingType(boxID);
                var tracesTypesVMs = _mapper.Map<IList<TrackingVM>>(tracesTypes);
                return tracesTypesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<TrackingApplicationService>("Unexpected error fetching GetAll TraceabilityVM", nameof(this.GetByBoxID), ex);
            }
        }

        public TrackingVM GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TrackingVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public TrackingTypeVM GetTrackTypeById(Guid guid)
        {
            try
            {
                var trackingType = _unitOfWork.Repository<TrackingType>().GetById(guid);
                var trackingTypeVM = _mapper.Map<TrackingTypeVM>(trackingType);
                return trackingTypeVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<TrackingApplicationService>("Unexpected error fetching GetAllTraceType", nameof(this.GetTrackTypeById), ex);
            }
        }

        public TrackingVM Update(TrackingVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<TrackingVM> UpdateAsync(TrackingVM updated)
        {
            throw new NotImplementedException();
        }
    }
}
