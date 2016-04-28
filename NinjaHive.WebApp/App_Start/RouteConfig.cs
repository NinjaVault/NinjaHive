using NinjaHive.WebApp.Helpers;
using NinjaHive.WebApp.Areas.Items.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace NinjaHive.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            var defaultUri = UrlProvider<EquipmentController>.GetRouteValues(c => c.Index());

            // Register an empty route to fix Area default route registration problems
            var defaultRoute = routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { area = defaultUri["area"], controller = defaultUri["controller"], action = defaultUri["action"], id = UrlParameter.Optional }
            );
            // area data token needs to be set for areas to work properly
            defaultRoute.DataTokens["area"] = defaultUri["area"];

            routes.MapRoute(
                name: "Default_NoArea",
                url: "{controller}/{action}/{id}",
                defaults: new {id = UrlParameter.Optional }
            );
        }
    }
}
