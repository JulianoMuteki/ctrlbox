using CtrlBox.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IBoxApplicationService : IApplicationServiceBase<BoxVM>
    {
        ICollection<BoxTypeVM> GetAllBoxesType();
        void AddBoxType(BoxTypeVM boxTypeVM);
        ICollection<BoxVM> BoxesParents();
        ICollection<BoxVM> GetBoxesByDeliveryID(Guid deliveryID);
        ICollection<BoxProductItemVM> GetBoxesBoxesProductItemsByDeliveryID(Guid guid);
        ICollection<BoxVM> GetBoxesByBoxWithChildren(Guid boxID);
        BoxVM GetBoxesByIDWithBoxTypeAndProductItems(Guid boxID);
        IEnumerable<BoxVM> GetBoxesParentsWithBoxTypeEndProduct();
    }
}
