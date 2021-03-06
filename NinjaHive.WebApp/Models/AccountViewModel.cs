﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using NinjaHive.Contract;
using NinjaHive.Core.Extensions;

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

    public class ResetPasswordViewModel
    {
        [HiddenInput]
        public string UserId { get; set; }
        [HiddenInput]
        public string PasswordResetToken { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "The password must have a minimum length of {0} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The passwords doesn't match!")]
        public string ConfirmPassword { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<Role> Roles { get; set; }

        public string RolesDisplay => string.Join(", ", this.Roles.Select(r => r.ToFriendlyString()));

        public bool EmailConfirmed { get; set; }
        public bool IsAdmin { get; set; }
    }
}
