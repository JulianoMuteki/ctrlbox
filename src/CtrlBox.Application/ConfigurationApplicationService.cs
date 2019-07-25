using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    public class ConfigurationApplicationService : IConfigurationApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConfigurationApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PictureVM Add(PictureVM entity)
        {
            try
            {
                var picture = _mapper.Map<Picture>(entity);
                _unitOfWork.Repository<Picture>().Add(picture);
               _unitOfWork.CommitSync();

                return _mapper.Map<PictureVM>(picture); ;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ConfigurationApplicationService>("Unexpected error fetching add picture", nameof(this.Add), ex);
            }
        }

        public Task<PictureVM> AddAsync(PictureVM entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<PictureVM> GetAll()
        {
            try
            {
                var pcitures = _unitOfWork.Repository<Picture>().GetAll();

                return _mapper.Map<List<PictureVM>>(pcitures);
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ConfigurationApplicationService>("Unexpected error fetching get picture", nameof(this.GetById), ex);
            }
        }

        public Task<ICollection<PictureVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public PictureVM GetById(Guid id)
        {
            try
            {
                var pciture = _unitOfWork.Repository<Picture>().GetById(id);
              
                return _mapper.Map<PictureVM>(pciture); ;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<ConfigurationApplicationService>("Unexpected error fetching get picture", nameof(this.GetById), ex);
            }
        }

        public Task<PictureVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public PictureVM Update(PictureVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<PictureVM> UpdateAsync(PictureVM updated)
        {
            throw new NotImplementedException();
        }
    }
}
