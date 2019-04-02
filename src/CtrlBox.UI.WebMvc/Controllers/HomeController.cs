using System.Web.Mvc;

namespace CtrlBox.UI.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public ActionResult Index()
        {
            return View();
        }
    }
}