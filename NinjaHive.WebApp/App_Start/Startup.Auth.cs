using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using NinjaHive.WebApp.Extensions;
using NinjaHive.WebApp.Identity;
using Owin;
using SimpleInjector;

namespace NinjaHive.WebApp
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app, Container container)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(container.GetInstance<ApplicationUserManager>);
            app.CreatePerOwinContext(container.GetInstance<ApplicationRoleManager>);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                SlidingExpiration = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(60),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.
                    OnValidateIdentity = async (context) =>
                        {
                            await SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                                validateInterval: TimeSpan.FromMinutes(5),
                                regenerateIdentity: (manager, user) =>
                                    user.GenerateUserIdentityAsync(manager, context.Identity.GetIsPersistent())
                            )(context);

                            var newResponseGrant = context.OwinContext.Authentication.AuthenticationResponseGrant;
                            if (newResponseGrant != null)
                            {
                                newResponseGrant.Properties.IsPersistent = context.Identity.GetIsPersistent();
                            }
                        }
                },
            });            
        }
    }
}