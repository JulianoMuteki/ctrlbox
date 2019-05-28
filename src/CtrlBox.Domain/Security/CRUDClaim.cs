using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CtrlBox.Domain.Security
{
    public enum CRUDClaim
    {
        [Display(Name = "Create")]
        Create,    
        [Display(Name = "Read")]
        Read,
        [Display(Name = "Update")]
        Update,
        [Display(Name = "Delete")]
        Delete
    }
}
