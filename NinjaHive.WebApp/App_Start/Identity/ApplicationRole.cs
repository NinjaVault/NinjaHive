using Microsoft.AspNet.Identity.EntityFramework;

namespace NinjaHive.WebApp.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
    }
}
