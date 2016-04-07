using System;
using System.Linq;
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

            var userRoles = httpContext.User.LoadUserRolesList();

            //returns true if userroles contains all of the specified roles
            var result = this.roles.All(role => userRoles.Contains(role));
            return result;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var redirectRoute = UrlProvider<ErrorsController>.GetRouteValues(c => c.UnauthorizedError());
            filterContext.Result = new RedirectToRouteResult(redirectRoute);
        }
    }
}