using System.Web.Mvc;
using NinjaHive.WebApp.Helpers;

namespace NinjaHive.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected virtual RedirectResult Home()
        {
            var homeUrl = UrlProvider<ItemsController>.GetUrl(c => c.Index());
            return Redirect(homeUrl);
        }

        protected virtual ActionResult NoResults()
        {
            return PartialView(Partials.NoResults);
        }

        protected virtual ActionResult DefaultError()
        {
            return Redirect(UrlProvider<ErrorsController>.GetUrl(c => c.DefaultError()));
        }
    }
}