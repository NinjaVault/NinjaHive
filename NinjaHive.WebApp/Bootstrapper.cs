using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using NinjaHive.BusinessLayer;
using NinjaHive.BusinessLayer.CrossCuttingConcerns;
using NinjaHive.BusinessLayer.QueryHandlers;
using NinjaHive.BusinessLayer.Services;
using NinjaHive.Core;
using NinjaHive.Core.Decorators;
using NinjaHive.Domain;
using NinjaHive.WebApp.Models.IdentityModels;
using Owin;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Extensions.LifetimeScoping;
using SimpleInjector.Integration.Web.Mvc;

namespace NinjaHive.WebApp
{
    public static class Bootstrapper
    {
        public static Container container;

        public static Container Initialize(IAppBuilder app)
        {
            AutoMapperConfiguration.Configure();
            
            container = GetInitializedContainer(app);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }

        public static Container GetInitializedContainer(IAppBuilder app)
        {
            container = new Container();
            container.Options.DefaultScopedLifestyle = new LifetimeScopeLifestyle();

            RegisterOwinAndIdentity(app);
            RegisterNinjaHiveDatabase();

            RegisterCommandHandlers();
            RegisterQueryHandlers();

            RegisterServices();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            return container;
        }

        private static void RegisterServices()
        {
            container.RegisterSingleton(typeof (IEntityMapper<>), typeof (EntitiesAutoMapper<>));
            container.RegisterSingleton(typeof (IEntityMapper<,>), typeof (EntitiesAutoMapper<,>));
        }

        private static void RegisterNinjaHiveDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["NinjaHiveContext"].ConnectionString;

            var dbContextRegistration = Lifestyle.Scoped.CreateRegistration(() => new NinjaHiveContext(connectionString), container);

            container.AddRegistration(typeof(NinjaHiveContext), dbContextRegistration);
            container.AddRegistration(typeof(DbContext), dbContextRegistration);
        }

        private static void RegisterCommandHandlers()
        {
            container.Register(typeof (ICommandHandler<>), Bootstrapper.GetAssemblies());

            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (ValidationCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (SaveChangesCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(LifetimeScopeCommandHandlerProxy<>), Lifestyle.Singleton);
        }

        private static void RegisterQueryHandlers()
        {
            container.Register(typeof(IQueryHandler<,>), typeof(GetEntityByIdQueryHandler<>));
            container.Register(typeof (IQueryHandler<,>), Bootstrapper.GetAssemblies());

            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(LifetimeScopeQueryHandlerProxy<,>), Lifestyle.Singleton);
        }

        private static void RegisterOwinAndIdentity(IAppBuilder app)
        {
            container.RegisterSingleton(app);

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

        public static IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}