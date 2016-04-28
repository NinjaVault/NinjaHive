using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NinjaHive.WebApp.Startup))]
namespace NinjaHive.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = Bootstrapper.Initialize(app);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, container);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureAuth(app, container);
        }
    }
}
