using System.ComponentModel.DataAnnotations;

namespace CtrlBox.UI.Web.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
