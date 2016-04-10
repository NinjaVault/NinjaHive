using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NinjaHive.Contract;
using NinjaHive.Core.Extensions;
using NinjaHive.WebApp.Identity;
using NinjaHive.WebApp.Models;

namespace NinjaHive.WebApp.Extensions
{
    public static class IdentityExtensions
    {
        public static IEnumerable<UserViewModel> GetAllUsers(this ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            return
                from user in userManager.Users.ToList() //load to memory, because linq2entities cannot handle IsInRole..
                let isAdmin = userManager.IsInRole(user.Id, Role.Admin)
                let roles = roleManager.GetUserRoles(user.Roles)
                orderby user.UserName ascending
                select new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    IsAdmin = isAdmin,
                    Roles = roles,
                };
        }

        public static IEnumerable<Role> GetUserRoles<TRole, TKey>(this RoleManager<TRole, TKey> manager,
            IEnumerable<IdentityUserRole<TKey>> roles)
            where TRole : class, IRole<TKey>
            where TKey : IEquatable<TKey>
        {
            var roleNames =
                from userRole in roles
                let role = manager.FindById(userRole.RoleId)
                select role.Name;

            return roleNames.ToRoles();
        }


        public static bool UserExists<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return userId != null && manager.FindById(userId) != null;
        }

        public static IEnumerable<Role> ToRoles(this IEnumerable<string> roles)
        {
            return
                from role in roles
                let trimmedRole = role.RemoveAllWhiteSpace()
                let parsedRole = Enum.Parse(typeof(Role), trimmedRole, ignoreCase: true)
                select (Role)parsedRole;
        }

        public static IdentityResult AddToRole<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, Role role)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager.AddToRole(userId, role.ToFriendlyString());
        }

        public static IdentityResult AddToRoles<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, params Role[] roles)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager.AddToRoles(userId, roles.Select(r => r.ToFriendlyString()).ToArray());
        }

        public static bool IsInRole<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, Role role)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager.IsInRole(userId, role.ToFriendlyString());
        }
    }
}
