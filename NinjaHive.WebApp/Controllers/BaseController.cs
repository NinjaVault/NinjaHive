using System.Web.Mvc;
using NinjaHive.WebApp.Helpers;

namespace NinjaHive.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected virtual ActionResult Home()
        {
            var homeUrl = UrlProvider<ItemsController>.GetUrl(c => c.Index());
            return Redirect(homeUrl);
        }

        protected virtual ActionResult NoResults()
        {
            return PartialView(Partials.NoResults);
        }
    }
}