using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NinjaHive.Contract;
using NinjaHive.Core.Extensions;
using NinjaHive.WebApp.Extensions;
using NinjaHive.WebApp.Filters;
using NinjaHive.WebApp.Helpers;
using NinjaHive.WebApp.Identity;
using NinjaHive.WebApp.Models;

namespace NinjaHive.WebApp.Controllers
{
    [Authorize]
    [RequireHttps]
    public class AccountController : BaseController
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly ApplicationUserManager userManager;

        public AccountController(IAuthenticationManager authenticationManager, ApplicationUserManager userManager)
        {
            this.authenticationManager = authenticationManager;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (this.authenticationManager.User.Identity.IsAuthenticated)
            {
                return Home();
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.FindAsync(model.Username, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : Home();
                }
            }

            ModelState.AddModelError("", "Unknown username and/or password!");
            return View(model);
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            this.authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent },
                        await user.GenerateUserIdentityAsync(this.userManager, isPersistent));
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ManageUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var result =
                    await this.userManager.ChangePasswordAsync(userId, viewModel.OldPassword, viewModel.NewPassword);

                if (result.Succeeded)
                {
                    this.authenticationManager.SignOut();
                    return Redirect(UrlProvider<AccountController>.GetUrl(c => c.ChangePasswordConfirmation()));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return this.View();
            }

            return View(viewModel);
        }

        public ActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        [Authorize]
        public async Task<JsonResult> ValidatePassword(string newPassword)
        {
            var result = await this.userManager.PasswordValidator.ValidateAsync(newPassword);

            return result.Succeeded
                ? this.Json(true, JsonRequestBehavior.AllowGet)
                : this.Json(
                    "The new password must meet the following requirements:\r\n" + string.Join("\r\n", result.Errors),
                    JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.authenticationManager.SignOut();
            return Home();
        }

        [AuthorizeRoles(Role.Admin)]
        public ActionResult ManageUsers()
        {
            var users = userManager.GetAllUsers().ToReadOnlyCollection();
            return View(users);
        }

        [AuthorizeRoles(Role.Admin)]
        public ActionResult CreateUser()
        {
            return View();
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = viewModel.Username,
                    Email = viewModel.Email,
                };

                var password = Membership.GeneratePassword(8, 1);
                var result = this.userManager.Create(user, password);
                if (result.Succeeded)
                {
                    var roleResult = this.userManager.AddToRoles(user.Id, Role.GameDesigner);
                    if (roleResult.Succeeded)
                    {
                        var mailToken = this.userManager.GenerateEmailConfirmationToken(user.Id);
                        var callbackUrl =
                            Url.GetFullyQualifiedActionLink<AccountController>(c => c.ConfirmEmail(user.Id, mailToken), Request.Url.Scheme);

                        this.userManager.SendEmail(user.Id, "Confirm your account",
                            $"Hello {user.UserName},"
                            + Environment.NewLine
                            + Environment.NewLine +
                            $"Please confirm your account by clicking <a href=\"{callbackUrl}\">here</a>."
                            + Environment.NewLine +
                            $"Your password is: <b>{password}</b>");

                        return Redirect(UrlProvider<AccountController>.GetUrl(c => c.ManageUsers()));
                    }
                    
                }
            }
            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult ConfirmEmail(string userId, string mailToken)
        {
            if (userId != null && mailToken != null)
            {
                var result = this.userManager.ConfirmEmail(userId, mailToken);
                if (result.Succeeded)
                {
                    return View();
                }
            }

            return Redirect(UrlProvider<ErrorsController>.GetUrl(c => c.DefaultError()));
        }
    }
}