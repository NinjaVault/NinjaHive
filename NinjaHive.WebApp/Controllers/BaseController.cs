using NinjaHive.WebApp.Areas.Items.Controllers;
using NinjaHive.WebApp.Helpers;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected virtual RedirectResult Home()
        {
            return Redirect<EquipmentController>(c => c.Index());
        }

        protected virtual RedirectResult Redirect<TController>(Expression<Action<TController>> action)
            where TController : Controller
        {
            var homeUrl = UrlProvider<TController>.GetUrl(action);
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