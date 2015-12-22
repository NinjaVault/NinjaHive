using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using NinjaHive.WebApp.Helpes;

namespace NinjaHive.WebApp.Extensions
{
    public static class UrlExtensions
    {
        public static string Action<TController>(this UrlHelper urlHelper, Expression<Action<TController>> expression)
            where TController : Controller
        {
            var routeValues = expression.GetRouteValues();
            return urlHelper.RouteUrl(routeValues);
        }

        public static MvcHtmlString HtmlActionLink<TController>(this HtmlHelper htmlHelper, string linkText, Expression<Action<TController>> expression)
            where TController : Controller
        {
            var routeValues = expression.GetRouteValues();
            return htmlHelper.RouteLink(linkText, routeValues);
        }

        private static RouteValueDictionary GetRouteValues<TController>(this Expression<Action<TController>> expression)
            where TController : Controller
        {
            return UrlProvider<TController>.GetRouteValues(expression);
        }
    }
}