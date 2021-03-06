﻿using System.Linq;
using System.Security.Claims;

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
    }
}