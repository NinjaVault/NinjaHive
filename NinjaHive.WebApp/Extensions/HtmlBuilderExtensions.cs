using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace NinjaHive.WebApp.Extensions
{
    public static class HtmlBuilderExtensions
    {
        public static MvcHtmlString BeginElement(this HtmlHelper htmlHelper,
                                                 string tagName,
                                                 object htmlAttributes = null)
        {
            return htmlHelper.BeginElement(tagName, htmlAttributes, false);
        }

        public static MvcHtmlString BeginElementSelfClose(this HtmlHelper htmlHelper,
                                                          string tagName,
                                                          object htmlAttributes = null)
        {
            return htmlHelper.BeginElement(tagName, htmlAttributes, true);
        }

        public static MvcHtmlString EndElement(this HtmlHelper helper, string tagName)
        {
            return MvcHtmlString.Create($"</{tagName}>");
        }


        static MvcHtmlString BeginElement(this HtmlHelper helper, string tagName, object htmlAttributes, bool selfClose)
        {
            var builder = new StringBuilder();
            builder.Append($"<{tagName}");

            if (htmlAttributes != null)
            {
                var attributeList = helper.MakeRouteValueDictionary(htmlAttributes);
                foreach (var attribute in attributeList)
                {
                    // Replace all underscores with dashes for valid HTML attribute names
                    var name = attribute.Key.Replace("_", "-");

                    // All values will be inserted inside single quotes, so escape all single quotes
                    var value = attribute.Value.ToString().Replace("'", "\\'");

                    builder.Append($" {name}='{value}'");
                }
            }

            if (selfClose)
            {
                builder.Append("/>");
            }
            else
            {
                builder.Append(">");
            }
            return MvcHtmlString.Create(builder.ToString());
        }

        public static RouteValueDictionary MakeRouteValueDictionary(this HtmlHelper helper, object target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            var routeValue = target as RouteValueDictionary;
            if (routeValue != null)
                return routeValue;

            // If they're a dictionary, create a new RouteValueDictionary with it
            var dictionary = target as IDictionary<string, object>;
            if (dictionary != null)
                return new RouteValueDictionary(dictionary);

            // Otherwise, create a RouteValueDictionary from the anonymous object
            return HtmlHelper.AnonymousObjectToHtmlAttributes(target);
        }
    }
}