using CtrlBox.UI.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CtrlBox.UI.WebMvc.EndPoints
{
    public class WebApiProduct : WebApiBase<ProdutoVM>
    {
        public WebApiProduct(string urlEndPoint, string controller)
            : base(urlEndPoint, controller)
        {

        }
    }
}