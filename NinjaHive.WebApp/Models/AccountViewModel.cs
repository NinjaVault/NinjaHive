using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "The password must have a minimum length of {0} characters.", MinimumLength = 6)]
        [Remote("ValidatePassword", "Account")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The passwords doesn't match!")]
        public string ConfirmPassword { get; set; }
    }
}
