using CtrlBox.UI.Web.Models.Role;
using System;
using System.ComponentModel.DataAnnotations;

namespace CtrlBox.UI.Web.Models.Manage
{
    public class RegisterViewModel
    {
        public Guid IdUser { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone is a Required field.")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression("^[01]?[- .]?\\(?[2-9]\\d{2}\\)?[- .]?\\d{3}[- .]?\\d{4}$",
        //        ErrorMessage = "Phone is required and must be properly formatted.")]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }


        public RoleViewModel RolesToUsersVM { get; set; }

        public RegisterViewModel()
        {
            this.RolesToUsersVM = new RoleViewModel();
        }
    }
}
