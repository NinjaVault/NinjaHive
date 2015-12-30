using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NinjaHive.WebApp.Helpers;
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
    }
}