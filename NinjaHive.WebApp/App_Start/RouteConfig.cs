using System.Web.Mvc;
using System.Web.Routing;
using NinjaHive.WebApp.Controllers;
using NinjaHive.WebApp.Services;

namespace NinjaHive.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var defaultUri = UrlProvider<EquipmentItemController>.GetRouteValues(c => c.Index());
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = defaultUri["controller"], action = defaultUri["action"], id = UrlParameter.Optional }
            );
        }
    }
}
