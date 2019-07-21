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

        public TraceabilityVM Add(TraceabilityVM entity)
        {
            try
            {
                var traceability = _mapper.Map<Traceability>(entity);

                //if (!boxType.ComponentValidator.Validate(boxType, new BoxTypeValidator()))
                //{
                //    throw new CustomException(string.Join(", ", boxType.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
                //}

                _unitOfWork.Repository<Traceability>().Add(traceability);
                _unitOfWork.Commit();

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

        public Task<TraceabilityVM> AddAsync(TraceabilityVM entity)
        {
            throw new NotImplementedException();
        }

        public void AddTraceType(TraceTypeVM entity)
        {
            try
            {
                var traceType = _mapper.Map<TraceType>(entity);

                //if (!boxType.ComponentValidator.Validate(boxType, new BoxTypeValidator()))
                //{
                //    throw new CustomException(string.Join(", ", boxType.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
                //}

                _unitOfWork.Repository<TraceType>().Add(traceType);
                _unitOfWork.Commit();
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

        public ICollection<TraceabilityVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TraceabilityVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ICollection<TraceTypeVM> GetAllTracesTypes()
        {
            try
            {
                var tracesTypes = _unitOfWork.Repository<TraceType>().GetAll();
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

        public ICollection<TraceabilityVM> GetByBoxID(Guid boxID)
        {
            try
            {
                var tracesTypes = _unitOfWork.RepositoryCustom<ITraceabilityRepository>().GetByBoxIDWithTraceType(boxID);
                var tracesTypesVMs = _mapper.Map<IList<TraceabilityVM>>(tracesTypes);
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

        public TraceabilityVM GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TraceabilityVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public TraceabilityVM Update(TraceabilityVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<TraceabilityVM> UpdateAsync(TraceabilityVM updated)
        {
            throw new NotImplementedException();
        }
    }
}
