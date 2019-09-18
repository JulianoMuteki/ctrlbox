using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IBoxApplicationService : IApplicationServiceBase<BoxVM>
    {
        ICollection<BoxTypeVM> GetAllBoxesType();
        void AddBoxType(BoxTypeVM boxTypeVM);
        ICollection<BoxVM> GetBoxesStockParents(Guid routeID);
        ICollection<BoxVM> GetBoxesByDeliveryID(Guid deliveryID);
        ICollection<OrderProductItemVM> GetOrderProductItemByDeliveryID(Guid guid);
        ICollection<BoxVM> GetBoxesByBoxWithChildren(Guid boxID);
        BoxVM GetBoxesByIDWithBoxTypeAndProductItems(Guid boxID);
        IEnumerable<BoxVM> GetBoxesParentsWithBoxTypeEndProduct();
        ICollection<BoxVM> FindBoxesAvailableWithProducts();
        ICollection<BoxVM> FindBoxesAvailableByBoxType(Guid guid);
        void GenarateBoxes(int nivel);
    }
}
