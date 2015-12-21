using System.ComponentModel.DataAnnotations;

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
}
