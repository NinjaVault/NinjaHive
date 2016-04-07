using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using NinjaHive.Contract;
using NinjaHive.Core.Extensions;
using NinjaHive.WebApp.Extensions;

namespace NinjaHive.WebApp.Services
{
    public class HttpWebUserContextWithRoles : IUserContextWithRoles
    {
        public string Id => HttpContext.Current.User.Identity.GetUserId();

        public string UserName => HttpContext.Current.User.Identity.GetUserName();

        public IReadOnlyCollection<Role> UserRoles
            => Roles.GetRolesForUser().ToRoles().ToReadOnlyCollection();
    }
}