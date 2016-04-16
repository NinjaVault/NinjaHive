using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NinjaHive.WebApp.Identity
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
}
