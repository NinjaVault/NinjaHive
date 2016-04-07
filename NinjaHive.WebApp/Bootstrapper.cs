using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
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
using NinjaHive.Core.Services;
using NinjaHive.Domain;
using NinjaHive.WebApp.Identity;
using NinjaHive.WebApp.Services;
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
            RegisterEmailServices();

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            return container;
        }

        private static void RegisterServices()
        {
            container.RegisterSingleton(typeof(IEntityMapper<>), typeof(EntitiesAutoMapper<>));
            container.RegisterSingleton(typeof(IEntityMapper<,>), typeof(EntitiesAutoMapper<,>));
            container.Register<IUserContext, HttpWebUserContext>(Lifestyle.Scoped);
            container.Register<IUserContextWithRoles, HttpWebUserContextWithRoles>(Lifestyle.Scoped);
            container.RegisterSingleton<ITimeProvider, SystemTimeProvider>();
            container.RegisterSingleton(typeof(IWriteOnlyRepository<>), typeof(WriteOnlyCommandRepository<>));
            container.RegisterSingleton<ILogger, DatabaseLogger>();
        }

        private static void RegisterEmailServices()
        {
#if DEBUG
            container.RegisterSingleton<IIdentityMessageService, FakeEmailService>();
#else
            container.RegisterSingleton<IIdentityMessageService>(new SendGridEmailService(
                mailAccount: ConfigurationManager.AppSettings["mailAccount"],
                mailPassword: ConfigurationManager.AppSettings["mailPassword"],
                fromAddress: ConfigurationManager.AppSettings["fromAddress"]));
#endif

        }

        private static void RegisterNinjaHiveDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["NinjaHiveContext"].ConnectionString;
            container.RegisterSingleton<IConnectionFactory>(new SqlConnectionFactory(connectionString));

            //this here is required because of EF's UnintentionalCodeFirstException
            connectionString += "MultipleActiveResultSets=True;App=NinjaHiveContext";
            connectionString = BuildEntityConnectionString(connectionString, "NinjaHiveEntities");

            var dbContextRegistration = Lifestyle.Scoped.CreateRegistration(
                () => new NinjaHiveContext(connectionString), container);

            container.AddRegistration(typeof(NinjaHiveContext), dbContextRegistration);
            container.AddRegistration(typeof(DbContext), dbContextRegistration);

            container.Register<IEntityEditHandler, EntityEditHandler>(Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(NinjaHiveRepository<>), Lifestyle.Scoped);
        }

        private static void RegisterCommandHandlers()
        {
            container.Register(typeof(ICommandHandler<>), Bootstrapper.GetAssemblies());

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(SaveChangesCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(LifetimeScopeCommandHandlerProxy<>), Lifestyle.Singleton);
        }

        private static void RegisterQueryHandlers()
        {
            container.RegisterSingleton<IQueryProcessor, QueryProcessor>();

            container.Register(typeof(IQueryHandler<,>), typeof(GetEntityByIdQueryHandler<>));
            container.Register(typeof(IQueryHandler<,>), Bootstrapper.GetAssemblies());

            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(LifetimeScopeQueryHandlerProxy<,>), Lifestyle.Singleton);
        }

        private static void RegisterOwinAndIdentity(IAppBuilder app)
        {
            container.RegisterSingleton(app);

            container.RegisterPerWebRequest<ApplicationUserManager>();

            var connectionString = ConfigurationManager.ConnectionStrings["Identity"].ConnectionString;
            container.RegisterPerWebRequest(
                () => new ApplicationDbContext(connectionString));

            container.RegisterPerWebRequest<IUserStore<ApplicationUser>>(
                () => new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()));

            container.RegisterPerWebRequest<IRoleStore<IdentityRole, string>>(
                () => new RoleStore<IdentityRole>(container.GetInstance<ApplicationDbContext>()));

            container.RegisterInitializer<ApplicationUserManager>(
                manager => ConfigureApplicationUserManager(manager, app));

            container.RegisterPerWebRequest<ApplicationRoleManager>();

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
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true,
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = false;
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

        public static string BuildEntityConnectionString(string connectionString, string modelName)
        {
            var builder = new EntityConnectionStringBuilder
            {
                Metadata = string.Format("res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", modelName),
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = connectionString
            };

            return builder.ToString();
        }
    }
}