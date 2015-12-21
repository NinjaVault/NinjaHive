using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

    public class DatabaseInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
#if DEBUG
            const string id = "1AB2BC08-35E1-4513-A757-ECC68405EBC8";
            const string username = "dev";
            const string securityStamp = "529FA8A6-7299-4D63-BA6F-496542B3015E";
            var password = new PasswordHasher().HashPassword("dev");

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    Id = id,
                    UserName = username,
                    PasswordHash = password,
                    SecurityStamp = securityStamp,
                    EmailConfirmed = true,
                });
#endif
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
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
}
