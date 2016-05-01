using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using NinjaHive.WebApp.Helpers;

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

        public static string GetFullyQualifiedActionLink<TController>(this UrlHelper urlHelper,
            Expression<Action<TController>> expression, string scheme)
            where TController : Controller
        {
            var routeValues = expression.GetRouteValues();
            return urlHelper.RouteUrl("Default", routeValues, scheme, string.Empty);
        }

        public static MvcHtmlString ActionLink<TController>(this HtmlHelper htmlHelper, string linkText, Expression<Action<TController>> expression)
                where TController : Controller
        {
            return htmlHelper.ActionLink(linkText, expression, null);
        }

        public static MvcHtmlString ActionLink<TController>(this HtmlHelper htmlHelper, string linkText, Expression<Action<TController>> expression, object htmlAttributes)
            where TController : Controller
        {
            var routeValues = expression.GetRouteValues();
            var htmlDictionary = new Dictionary<string, object>();
            if (htmlAttributes != null)
            {
                foreach (var property in htmlAttributes.GetType().GetProperties())
                {
                    var value = htmlAttributes.GetType().GetProperty(property.Name).GetValue(htmlAttributes);
                    htmlDictionary.Add(property.Name, value);
                }
            }
            return htmlHelper.RouteLink(linkText, routeValues, htmlDictionary);
        }

        private static RouteValueDictionary GetRouteValues<TController>(this Expression<Action<TController>> expression)
            where TController : Controller
        {
            return UrlProvider<TController>.GetRouteValues(expression);
        }
    }
}