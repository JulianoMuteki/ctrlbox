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
using CtrlBox.Domain.Interfaces.Repository;
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
            try
            {
                var box = _mapper.Map<Box>(entity);
                if (box.ProductID != null && box.ProductID != Guid.Empty)
                    AddBoxHasProduct(entity.RangeProductsItems, box);
                else
                    AddBoxWithoutProduct(entity.ChildrenBoxesID, box);

                return entity;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching add product", nameof(this.Add), ex);
            }
        }
        private void AddBoxWithoutProduct(string[] boxesChildrenID, Box box)
        {
            var boxesChildren = _unitOfWork.Repository<Box>().FindAll(x => boxesChildrenID.Any(id => new Guid(id) == x.Id)).ToList();
            var boxesChildrenWithFather = box.AddChildren(boxesChildren);

            _unitOfWork.Repository<Box>().Add(box);
            _unitOfWork.Repository<Box>().UpdateRange(boxesChildrenWithFather);
            _unitOfWork.Commit();
        }
        private void AddBoxHasProduct(int rangeProductsItems, Box box)
        {
            var productItems = _unitOfWork.Repository<ProductItem>().FindAll(x => x.ProductID == box.ProductID).OrderByDescending(x => x.CreationDate).Take(rangeProductsItems).ToList();
            box.LoadProductItems(productItems);

            if (!box.ComponentValidator.Validate(box, new BoxValidator()))
            {
                throw new CustomException(string.Join(", ", box.ComponentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage)));
            }
            _unitOfWork.Repository<ProductItem>().UpdateRange(productItems);
            _unitOfWork.Repository<Box>().Add(box);
            _unitOfWork.Commit();
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

                if (!boxType.ComponentValidator.Validate(boxType, new BoxTypeValidator()))
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
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching add product", nameof(this.Add), ex);
            }
        }

        public ICollection<BoxVM> BoxesParents()
        {
            try
            {
                var boxes = _unitOfWork.RepositoryCustom<IBoxRepository>().GetBoxesParentsWithBoxType();
                var boxesVMs = _mapper.Map<IList<BoxVM>>(boxes);
                return boxesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes", nameof(this.GetAll), ex);
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
            try
            {
                var boxes = _unitOfWork.RepositoryCustom<IBoxRepository>().GetAllWithBoxTypeAndProduct();
                var boxesVMs = _mapper.Map<IList<BoxVM>>(boxes);
                return boxesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxApplicationService>("Unexpected error fetching all boxes", nameof(this.GetAll), ex);
            }
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
