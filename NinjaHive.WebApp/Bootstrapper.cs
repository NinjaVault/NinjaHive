using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using NinjaHive.BusinessLayer.CrossCuttingConcerns;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.WebApp.Models.IdentityModels;
using Owin;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Extensions;
using SimpleInjector.Integration.Web.Mvc;

namespace NinjaHive.WebApp
{
    public static class Bootstrapper
    {
        public static Container container;

        public static Container Initialize(IAppBuilder app)
        {
            container = GetInitializedContainer(app);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }

        public static Container GetInitializedContainer(IAppBuilder app)
        {
            container = new Container();

            RegisterOwinAndIdentity(app);
            RegisterNinjaHiveDatabase();

            RegisterCommandHandlers();
            RegisterQueryHandlers();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            return container;
        }

        private static void RegisterNinjaHiveDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["NinjaHiveContext"].ConnectionString;

            container.RegisterPerWebRequest<NinjaHiveContext>(
                () => new NinjaHiveContext(connectionString));
        }

        private static void RegisterCommandHandlers()
        {
            container.RegisterManyForOpenGeneric(typeof (ICommandHandler<>),
                AppDomain.CurrentDomain.GetAssemblies());

            container.RegisterDecorator(typeof (ICommandHandler<>),
                typeof (ValidationCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof (ICommandHandler<>),
                typeof (SaveChangesCommandHandlerDecorator<>));
        }

        private static void RegisterQueryHandlers()
        {
            container.RegisterManyForOpenGeneric(typeof (IQueryHandler<,>),
                AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void RegisterOwinAndIdentity(IAppBuilder app)
        {
            container.RegisterSingle(app);

            container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<ApplicationSignInManager>();
            
            container.RegisterPerWebRequest(
                () => new ApplicationDbContext("DefaultConnection"));

            container.RegisterPerWebRequest<IUserStore<ApplicationUser>>(
                () => new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()));

            container.RegisterInitializer<ApplicationUserManager>(
                manager => ConfigureApplicationUserManager(manager, app));

            container.RegisterPerWebRequest<IAuthenticationManager>(
                () => container.IsVerifying()
                    ? new OwinContext().Authentication
                    : HttpContext.Current.GetOwinContext().Authentication);
        }

        public static void ConfigureApplicationUserManager(ApplicationUserManager manager, IAppBuilder app)
        {
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            //
            var dataProtectionProvider = app.GetDataProtectionProvider();
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(
                        dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }
    }
}