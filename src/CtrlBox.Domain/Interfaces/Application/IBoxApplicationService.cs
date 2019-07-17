using CtrlBox.Application.ViewModel;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IBoxApplicationService : IApplicationServiceBase<BoxVM>
    {
        ICollection<BoxTypeVM> GetAllBoxesType();
        void AddBoxType(BoxTypeVM boxTypeVM);
        ICollection<BoxVM> BoxesParents();
    }
}
