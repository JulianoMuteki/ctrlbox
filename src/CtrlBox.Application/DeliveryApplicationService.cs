﻿using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using CtrlBox.Domain.Interfaces.Base;
using CtrlBox.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtrlBox.Application
{
    public class DeliveryApplicationService : IDeliveryApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeliveryApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public DeliveryVM Add(DeliveryVM entity)
        {
            try
            {
                var delivery = _mapper.Map<Delivery>(entity);

                foreach (var item in entity.BoxesTypes)
                {              
                    var boxesReadyToDelivery = _unitOfWork.Repository<Box>().FindAll(x => x.BoxTypeID == new Guid(item.DT_RowId) && x.BoxParentID == null).OrderByDescending(x=>x.DateModified).Take(item.QuantityToDelivery).ToList();
                    delivery.LoadBox(boxesReadyToDelivery);
                }

                _unitOfWork.Repository<Delivery>().Add(delivery);
                _unitOfWork.Commit();

                return entity;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryApplicationService>("Unexpected error fetching add delivery", nameof(this.Add), ex);
            }
        }

        public Task<DeliveryVM> AddAsync(DeliveryVM entity)
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

        public void FinalizeDelivery(Guid deliveryID)
        {
            try
            {
                var delivery = _unitOfWork.Repository<Delivery>().GetById(deliveryID);
                delivery.FinalizeDelivery();
                _unitOfWork.Repository<Delivery>().Update(delivery);
                _unitOfWork.Commit();
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryApplicationService>("Unexpected error fetching put delivery", nameof(this.FinalizeDelivery), ex);
            }
        }

        public ICollection<DeliveryVM> GetAll()
        {
            try
            {
                var deliveries = _unitOfWork.RepositoryCustom<IDeliveryRepository>().GetDeliveryRouteLoad();

                var deliveriesVMs = _mapper.Map<IList<DeliveryVM>>(deliveries);
                return deliveriesVMs;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryApplicationService>("Unexpected error fetching get delivery", nameof(this.GetAll), ex);
            }
        }

        public Task<ICollection<DeliveryVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public DeliveryVM GetById(Guid id)
        {
            try
            {
                var delivery = _unitOfWork.Repository<Delivery>().GetById(id);
                var deliveryVM = _mapper.Map<DeliveryVM>(delivery);
                return deliveryVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryApplicationService>("Unexpected error fetching get delivery", nameof(this.GetById), ex);
            }
        }

        public Task<DeliveryVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<DeliveryVM> GetByUserId(Guid userId)
        {
            try
            {
                var delivery = _unitOfWork.RepositoryCustom<IDeliveryRepository>().GetDeliveryByUserWithRoute(userId);

                var deliveriesVM = _mapper.Map<IList<DeliveryVM>>(delivery);
                return deliveriesVM;
            }
            catch (CustomException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw CustomException.Create<DeliveryApplicationService>("Unexpected error fetching get delivery", nameof(this.GetByIdAsync), ex);
            }
        }

        public DeliveryVM GetResumeDeliveryById(Guid deliveryID)
        {
            {
                try
                {
                    var delivery = _unitOfWork.RepositoryCustom<IDeliveryRepository>().GetResumeDeliveryById(deliveryID);

                    var deliveryVM = _mapper.Map<DeliveryVM>(delivery);
                    return deliveryVM;
                }
                catch (CustomException exc)
                {
                    throw exc;
                }
                catch (Exception ex)
                {
                    throw CustomException.Create<DeliveryApplicationService>("Unexpected error fetching get delivery", nameof(this.GetByIdAsync), ex);
                }
            }
        }

        public DeliveryVM Update(DeliveryVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<DeliveryVM> UpdateAsync(DeliveryVM updated)
        {
            throw new NotImplementedException();
        }
    }
}
