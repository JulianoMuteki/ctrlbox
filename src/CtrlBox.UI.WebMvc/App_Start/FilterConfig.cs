using System.Web;
using System.Web.Mvc;

namespace CtrlBox.UI.WebMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
