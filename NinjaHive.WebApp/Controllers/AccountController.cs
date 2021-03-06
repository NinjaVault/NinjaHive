﻿using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NinjaHive.Contract;
using NinjaHive.Core.Extensions;
using NinjaHive.Core.Helpers;
using NinjaHive.WebApp.Extensions;
using NinjaHive.WebApp.Filters;
using NinjaHive.WebApp.Helpers;
using NinjaHive.WebApp.Identity;
using NinjaHive.WebApp.Models;
using NinjaHive.WebApp.Properties;

namespace NinjaHive.WebApp.Controllers
{
    [Authorize]
    [RequireHttps]
    public class AccountController : BaseController
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationRoleManager roleManager;

        public AccountController(
            IAuthenticationManager authenticationManager,
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            this.authenticationManager = authenticationManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
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

        [Authorize]
        public ActionResult LogOff()
        {
            this.authenticationManager.SignOut();
            return Home();
        }

        [AuthorizeRoles(Role.Admin)]
        public ActionResult ManageUsers()
        {
            var users = userManager.GetAllUsers(this.roleManager).ToReadOnlyCollection();
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

                var password = PasswordGenerator.GeneratePassword();
                var result = this.userManager.Create(user, password);
                if (result.Succeeded)
                {
                    var roleResult = this.userManager.AddToRoles(user.Id, Role.GameDesigner);
                    if (roleResult.Succeeded)
                    {
                        this.SendEmailConfirmation(user.Id, user.UserName, password);
                        return Redirect(UrlProvider<AccountController>.GetUrl(c => c.ManageUsers()));
                    }
                }
            }
            return View(viewModel);
        }

        [AuthorizeRoles(Role.Admin)]
        public ActionResult SendEmailConfirmation(string userId)
        {
            var user = this.userManager.FindById(userId);
            if (user != null && !user.EmailConfirmed)
            {
                var token = this.userManager.GeneratePasswordResetToken(userId);
                var password = PasswordGenerator.GeneratePassword();
                var result = this.userManager.ResetPassword(userId, token, password);
                if (result.Succeeded)
                {
                    this.SendEmailConfirmation(userId, user.UserName, password);
                    return View(user);
                }
            }
            return base.DefaultError();
        }

        public ActionResult ResetPasswordConfirmation(string userId)
        {
            var user = this.userManager.FindById(userId);
            if (user != null && user.EmailConfirmed)
            {
                var token = this.userManager.GeneratePasswordResetToken(userId);
                var callbackUrl = Url.GetFullyQualifiedActionLink<AccountController>(
                    c => c.ResetPassword(user.Id, token), Request.Url.Scheme);

                this.userManager.SendEmail(userId, "Reset your password",
                    string.Format(Resources.ResetPasswordMailTemplate, user.UserName, callbackUrl));

                return View(user);
            }
            return base.DefaultError();
        }

        private void SendEmailConfirmation(string userId, string userName, string password)
        {
            var mailToken = this.userManager.GenerateEmailConfirmationToken(userId);
            var callbackUrl =
                Url.GetFullyQualifiedActionLink<AccountController>(c => c.ConfirmEmail(userId, mailToken), Request.Url.Scheme);

            this.userManager.SendEmail(userId, "Confirm your account",
                string.Format(Resources.AccountConfirmationMailTemplate, userName, callbackUrl, password));
        }

        [AuthorizeRoles(Role.Admin)]
        public ActionResult EditUser(string userId)
        {
            var user = this.userManager.FindById(userId);
            if (user != null)
            {
                var viewModel = new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                };
                return View(viewModel);
            }

            return base.DefaultError();
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = this.userManager.FindById(viewModel.Id);
                if (user != null)
                {
                    user.UserName = viewModel.Username;
                    user.Email = viewModel.Email;

                    var result = this.userManager.Update(user);
                    if (result.Succeeded)
                    {
                        if (User.Identity.GetUserId() == user.Id)
                        {
                            await SignInAsync(user, false);
                        }
                        return Redirect(UrlProvider<AccountController>.GetUrl(c => c.ManageUsers()));
                    }
                }
            }
            
            return View(viewModel);
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost]
        public ActionResult DeleteUser(string userId)
        {
            var user = this.userManager.FindById(userId);
            if (user != null && !this.userManager.IsInRole(userId, Role.Admin))
            {
                var result = this.userManager.Delete(user);
                if (result.Succeeded)
                {
                    return Redirect(UrlProvider<AccountController>.GetUrl(c => c.ManageUsers()));
                }
            }
            return base.DefaultError();
        }

        [AllowAnonymous]
        public ActionResult ConfirmEmail(string userId, string mailToken)
        {
            if (mailToken != null && this.userManager.UserExists(userId))
            {
                var result = this.userManager.ConfirmEmail(userId, mailToken);
                if (result.Succeeded)
                {
                    return View();
                }
            }

            return base.DefaultError();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string userId, string passwordToken)
        {
            if (passwordToken != null && this.userManager.UserExists(userId))
            {
                var viewModel = new ResetPasswordViewModel
                {
                    UserId = userId,
                    PasswordResetToken = passwordToken,
                };
                return View(viewModel);
            }
            return base.DefaultError();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (this.userManager.UserExists(viewModel.UserId))
                {
                    var result = this.userManager.ResetPassword(
                        viewModel.UserId, viewModel.PasswordResetToken, viewModel.NewPassword);

                    if (result.Succeeded)
                    {
                        return base.Home();
                    }
                }

                return base.DefaultError();
            }
            return View(viewModel);
        }
    }
}