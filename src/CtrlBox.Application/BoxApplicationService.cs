using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;

namespace CtrlBox.Application
{
    public class BoxApplicationService: IBoxApplicationService
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
