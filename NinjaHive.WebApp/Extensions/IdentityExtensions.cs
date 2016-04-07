using System;
using System.Collections.Generic;
using System.Linq;
using NinjaHive.Contract;
using NinjaHive.WebApp.Models;

namespace NinjaHive.WebApp.Extensions
{
    public static class IdentityExtensions
    {
        public static IEnumerable<UserViewModel> GetAllUsers(this ApplicationUserManager userManager)
        {
            return
                from user in userManager.Users
                select new UserViewModel
                {
                    Username = user.UserName,
                    Email = user.Email,
                };
        }

        public static IEnumerable<Role> ToRoles(this IEnumerable<string> roles)
        {
            return
                from role in roles
                let trimmedRole = role.Trim()
                let parsedRole = Enum.Parse(typeof(Role), trimmedRole, ignoreCase: true)
                select (Role)parsedRole;
        }
    }
}
