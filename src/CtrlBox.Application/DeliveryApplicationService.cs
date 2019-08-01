using AutoMapper;
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

                foreach (BoxTypeVM boxType in entity.BoxesTypes)
                {
                    var boxesReadyToDelivery = _unitOfWork.RepositoryCustom<IBoxRepository>().GetBoxesByBoxTypeIDWithProductItems(new Guid(boxType.DT_RowId), boxType.QuantityToDelivery);

                    delivery.ShippingBoxes(boxesReadyToDelivery);
                }
                var lista = delivery.BoxesProductItems.ToList();
                delivery.BoxesProductItems.Clear();
                _unitOfWork.Repository<Delivery>().Add(delivery);
                _unitOfWork.Repository<BoxProductItem>().UpdateRange(lista);

                _unitOfWork.CommitSync();

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
               _unitOfWork.CommitSync();
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

        public void MakeDelivery(DeliveryVM deliveryVM)
        {
            try
            {
                var delivery = _mapper.Map<Delivery>(deliveryVM);
                _unitOfWork.SetTrackAll();
                var boxes = _unitOfWork.RepositoryCustom<IBoxRepository>().GetBoxesByDeliveryIDWithProductItems(delivery.Id);

                foreach (var deliveryProduct in delivery.DeliveriesProducts)
                {
                    foreach (var box in boxes)
                    {
                        box.DoDelivery(deliveryProduct.ProductID, deliveryProduct.Amount);
                    }
                }

                _unitOfWork.Repository<Box>().UpdateRange(boxes);
                _unitOfWork.Repository<DeliveryProduct>().AddRange(delivery.DeliveriesProducts);
                _unitOfWork.CommitSync();
            }
            catch (CustomException exc)
            {
                _unitOfWork.Rollback();
                throw exc;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw CustomException.Create<SaleApplicationService>("Unexpected error fetching add sale", nameof(this.Add), ex);
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
