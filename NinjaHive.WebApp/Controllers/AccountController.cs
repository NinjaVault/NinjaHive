using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NinjaHive.WebApp.Models;

namespace NinjaHive.WebApp.Controllers
{
    [Authorize]
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