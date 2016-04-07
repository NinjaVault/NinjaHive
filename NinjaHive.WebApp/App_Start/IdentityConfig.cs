using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using NinjaHive.WebApp.Extensions;

namespace NinjaHive.WebApp
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(string connectionString)
            : base(connectionString, throwIfV1Schema: false)
        {
            Database.SetInitializer(new DatabaseInitializer());
        }
    }

    public class DatabaseInitializer :
#if DEBUG
        DropCreateDatabaseAlways<ApplicationDbContext>
#else
        CreateDatabaseIfNotExists<ApplicationDbContext>
#endif
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            this.AddRoleIfNotExist(roleManager, "Admin");
            this.AddRoleIfNotExist(roleManager, "Game Designer");

#if DEBUG
            var user = userManager.FindByName("admin");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "admin",
                };
                var result = userManager.Create(user, "admin");
                if (result.Succeeded)
                {
                    userManager.AddToRoles(user.Id, "Admin", "Game Designer");
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

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }
    }

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, bool isPersistent)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.SetIsPersistent(isPersistent);
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
    }
}
