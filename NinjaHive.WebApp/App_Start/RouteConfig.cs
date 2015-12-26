using System.Web.Mvc;
using System.Web.Routing;
using NinjaHive.WebApp.Controllers;
using NinjaHive.WebApp.Helpers;

namespace NinjaHive.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            var defaultUri = UrlProvider<ItemsController>.GetRouteValues(c => c.Index());
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = defaultUri["controller"], action = defaultUri["action"], id = UrlParameter.Optional }
            );
        }
    }
}
