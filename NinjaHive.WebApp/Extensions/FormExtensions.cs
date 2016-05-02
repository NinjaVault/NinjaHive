using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using NinjaHive.WebApp.Helpers;

namespace NinjaHive.WebApp.Extensions
{
    public static class FormExtensions
    {
        public static MvcForm BeginForm<TController>(this HtmlHelper htmlHelper, Expression<Action<TController>> expression)
            where TController : Controller
        {
            return htmlHelper.BeginForm(expression, null);
        }

        public static MvcForm BeginForm<TController>(this HtmlHelper htmlHelper, Expression<Action<TController>> expression, object htmlAttributes)
            where TController : Controller
        {
            var urlParts = UrlProvider<TController>.GetRouteValues(expression);

            var controller = urlParts["controller"].ToString();
            var action = urlParts["action"].ToString();
            return htmlHelper.BeginForm(action, controller, FormMethod.Post, htmlAttributes);
        }
    }
}
