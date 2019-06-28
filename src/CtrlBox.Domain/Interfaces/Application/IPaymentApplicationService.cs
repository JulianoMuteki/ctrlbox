using CtrlBox.Application.ViewModel;
using System.Collections.Generic;

namespace CtrlBox.Domain.Interfaces.Application
{
    public interface IPaymentApplicationService : IApplicationServiceBase<PaymentVM>
    {
        ICollection<PaymentMethodVM> GetPayMethods();
    }
}
