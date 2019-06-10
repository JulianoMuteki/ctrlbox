using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtrlBox.UI.Web.Models.Role
{
    public class DropDpwnViewModel
    {
        //       	"id": "d7a1f389-92fa-41be-2884-08d6eb8e5b98",
        //"text": "Admin",
        //"element": [{}],
        //"disabled": false,
        //"locked": false


        public string id { get; set; }
        public string text { get; set; }
        public string[] element { get; set; }
        public bool disable { get; set; }
        public bool locked { get; set; }

    }
}
