using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using NinjaHive.Contract;

namespace NinjaHive.WebApp.Extensions
{
    /// <summary>
    /// http://stackoverflow.com/questions/23983726/expiretimespan-ignored-after-regenerateidentity-validateinterval-duration-in-m
    /// </summary>
    public static class ClaimsIdentityExtensions
    {
        private const string PersistentLoginClaimType = "PersistentLogin";

        public static bool GetIsPersistent(this ClaimsIdentity identity)
        {
            return identity.Claims.FirstOrDefault(c => c.Type == PersistentLoginClaimType) != null;
        }

        public static void SetIsPersistent(this ClaimsIdentity identity, bool isPersistent)
        {
            var claim = identity.Claims.FirstOrDefault(c => c.Type == PersistentLoginClaimType);

            if (isPersistent && claim == null)
            {
                identity.AddClaim(new Claim(PersistentLoginClaimType, bool.TrueString));
            }
            else if (claim != null)
            {
                identity.RemoveClaim(claim);
            }
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