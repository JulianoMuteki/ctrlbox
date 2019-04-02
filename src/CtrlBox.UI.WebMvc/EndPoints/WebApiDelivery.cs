using CtrlBox.Application.ViewModel;

namespace CtrlBox.UI.WebMvc.EndPoints
{
    public class WebApiDelivery : WebApiBase<DeliveryVM>
    {
        public WebApiDelivery(string urlEndPoint, string controller)
            : base(urlEndPoint, controller)
        {

        }
    }
}