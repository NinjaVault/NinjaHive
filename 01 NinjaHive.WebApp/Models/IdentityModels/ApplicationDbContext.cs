using Microsoft.AspNet.Identity.EntityFramework;

namespace NinjaHive.WebApp.Models.IdentityModels
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(string connectionString)
            : base(connectionString, throwIfV1Schema: false)
        {
        }
    }
}