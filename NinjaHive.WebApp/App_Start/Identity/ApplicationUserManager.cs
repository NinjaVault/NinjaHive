using Microsoft.AspNet.Identity;

namespace NinjaHive.WebApp.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IIdentityMessageService emailService)
            : base(store)
        {
            this.EmailService = emailService;
        }
    }
}