using NinjaHive.WebApp.Areas.Items.Controllers;
using NinjaHive.WebApp.Helpers;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items
{
    public class ItemsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Items";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            var defaultUri = UrlProvider<EquipmentController>.GetRouteValues(c => c.Index());

            context.MapRoute(
                "Items_default",
                "Items/{controller}/{action}/{id}",
                new { area = defaultUri["area"], controller = defaultUri["controller"], action = defaultUri["action"], id = UrlParameter.Optional }
            );
        }
    }
}