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

        public OrderVM Add(OrderVM entity)
        {
            try
            {
                var delivery = _mapper.Map<Order>(entity);

                foreach (BoxTypeVM boxType in entity.BoxesTypes)
                {
                    var boxesReadyToDelivery = _unitOfWork.RepositoryCustom<IBoxRepository>().GetBoxesByBoxTypeIDWithProductItems(new Guid(boxType.DT_RowId), boxType.QuantityToDelivery);

                    delivery.AddOrdersBoxes(boxesReadyToDelivery);
                }
                var lista = delivery.BoxesProductItems.ToList();
                delivery.BoxesProductItems.Clear();
                _unitOfWork.Repository<Order>().Add(delivery);
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

        public Task<OrderVM> AddAsync(OrderVM entity)
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
                var delivery = _unitOfWork.Repository<Order>().GetById(deliveryID);
                delivery.FinalizeDelivery();
                _unitOfWork.Repository<Order>().Update(delivery);
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

        public void FinishDelivery(Guid orderID, bool hasCrossDocking)
        {
            try
            {
                _unitOfWork.SetTrackAll();
                var order = _unitOfWork.Repository<Order>().GetById(orderID);
                var boxes = _unitOfWork.RepositoryCustom<IBoxRepository>().GetBoxesByDeliveryIDWithProductItems(orderID);

                foreach (var box in boxes)
                {
                    box.FinishDelivery(hasCrossDocking);
                }
                order.Close();

                _unitOfWork.Repository<Box>().UpdateRange(boxes);
                _unitOfWork.Repository<Order>().Update(order);
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
                throw CustomException.Create<SaleApplicationService>("Unexpected error fetching add sale", nameof(this.FinishDelivery), ex);
            }
        }

        public ICollection<OrderVM> GetAll()
        {
            try
            {
                var deliveries = _unitOfWork.RepositoryCustom<IDeliveryRepository>().GetDeliveryRouteLoad();

                var deliveriesVMs = _mapper.Map<IList<OrderVM>>(deliveries);
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

        public Task<ICollection<OrderVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public OrderVM GetById(Guid id)
        {
            try
            {
                var delivery = _unitOfWork.Repository<Order>().GetById(id);
                var deliveryVM = _mapper.Map<OrderVM>(delivery);
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

        public Task<OrderVM> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<OrderVM> GetByUserId(Guid userId)
        {
            try
            {
                var delivery = _unitOfWork.RepositoryCustom<IDeliveryRepository>().GetDeliveryByUserWithRoute(userId);

                var deliveriesVM = _mapper.Map<IList<OrderVM>>(delivery);
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

        public OrderVM GetResumeDeliveryById(Guid deliveryID)
        {
            {
                try
                {
                    var delivery = _unitOfWork.RepositoryCustom<IDeliveryRepository>().GetResumeDeliveryById(deliveryID);

                    var deliveryVM = _mapper.Map<OrderVM>(delivery);
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

        public void MakeDelivery(OrderVM orderVM, Guid trackingTypeID)
        {
            try
            {
                var order = _mapper.Map<Order>(orderVM);
                _unitOfWork.SetTrackAll();
                var boxes = _unitOfWork.RepositoryCustom<IBoxRepository>().GetBoxesByDeliveryIDWithProductItems(order.Id);

                foreach (var deliveryDetail in order.DeliveriesDetails)
                {
                    foreach (var box in boxes)
                    {
                        deliveryDetail.MakeDeliveryBox(box);

                        box.AddTracking(trackingTypeID, deliveryDetail.ClientID);
                        box.AddTrackingProductItems(trackingTypeID, deliveryDetail.ClientID);
                    }
                }

                _unitOfWork.Repository<Box>().UpdateRange(boxes);
                _unitOfWork.Repository<DeliveryDetail>().AddRange(order.DeliveriesDetails);
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
                throw CustomException.Create<SaleApplicationService>("Unexpected error fetching add sale", nameof(this.MakeDelivery), ex);
            }
        }

        public OrderVM Update(OrderVM updated)
        {
            throw new NotImplementedException();
        }

        public Task<OrderVM> UpdateAsync(OrderVM updated)
        {
            throw new NotImplementedException();
        }
    }
}
