using System;
using System.Web;
using System.Web.Mvc;
using NinjaHive.Contract;
using NinjaHive.WebApp.Controllers;
using NinjaHive.WebApp.Extensions;
using NinjaHive.WebApp.Helpers;

namespace NinjaHive.WebApp.Filters
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private readonly Role[] roles;

        public AuthorizeRolesAttribute(params Role[] roles)
        {
            this.roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            return httpContext.User.IsInRoles(this.roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var redirectRoute = UrlProvider<ErrorsController>.GetRouteValues(c => c.UnauthorizedError());
            filterContext.Result = new RedirectToRouteResult(redirectRoute);
        }
    }
}