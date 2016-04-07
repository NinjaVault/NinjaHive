using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using NinjaHive.Contract;
using NinjaHive.Core.Extensions;

namespace NinjaHive.WebApp.Extensions
{
    public static class HttpExtensions
    {
        public static IEnumerable<Role> LoadUserRolesList(this IPrincipal userPrincipal)
        {
            return
                from role in Enum.GetNames(typeof(Role))
                where userPrincipal.IsInRole(role.ToFriendlyString())
                select (Role)Enum.Parse(typeof(Role), role);
        }
    }
}
