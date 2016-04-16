using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NinjaHive.Contract;
using NinjaHive.Core.Extensions;

namespace NinjaHive.WebApp.Identity
{
    public class DatabaseInitializer :
#if DEBUG
        DropCreateDatabaseAlways<ApplicationDbContext>
#else
        CreateDatabaseIfNotExists<ApplicationDbContext>
#endif
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            var roles = Enum.GetNames(typeof(Role)).Select(FriendlyExtensions.ToFriendlyString).ToArray();
            foreach (var role in roles)
            {
                this.AddRoleIfNotExist(roleManager, role);
            }

#if DEBUG
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindByName("admin");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@ninjahive.net",
                    EmailConfirmed = true,
                };
                var result = userManager.Create(user, "admin");
                if (result.Succeeded)
                {
                    userManager.AddToRoles(user.Id, roles);
                }
            }
#endif
            base.Seed(context);
        }

        private void AddRoleIfNotExist(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }
        }
    }
}
