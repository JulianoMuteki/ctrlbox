using System.ComponentModel.DataAnnotations;

namespace CtrlBox.UI.Web.Models.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
