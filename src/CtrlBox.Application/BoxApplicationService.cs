using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Domain.Validations;

namespace CtrlBox.Application
{
    public class BoxApplicationService : IBoxApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BoxApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BoxVM Add(BoxVM entity)
        {
            throw new NotImplementedException();
        }

        public Task<BoxVM> AddAsync(BoxVM entity)
        {
            throw new NotImplementedException();
        }

        public void AddBoxType(BoxTypeVM entity)
        {
            try
            {
                var boxType = _mapper.Map<BoxType>(entity);

                if(!boxType.ComponentValidator.Validate(boxType, new BoxTypeValidator()))
                {
                    throw new CustomException(string.Join(", ", boxType.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
                }

                _unitOfWork.Repository<BoxType>().Add(boxType);
                _unitOfWork.Commit();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ClientApplicationService>("Unexpected error fetching add product", nameof(this.Add), ex);
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

        public ICollection<BoxVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<BoxVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ICollection<BoxTypeVM> GetAllBoxesType()
        {
            try
            {
                var boxesType = _unitOfWork.Repository<BoxType>().GetAll();
                var boxesTypeVMs = _mapper.Map<IList<BoxTypeVM>>(boxesType);
                return boxesTypeVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes type", nameof(this.GetAllBoxesType), ex);
            }
        }

        public BoxVM GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BoxVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public BoxVM Update(BoxVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<BoxVM> UpdateAsync(BoxVM updated)
        {
            throw new NotImplementedException();
        }
    }
}
