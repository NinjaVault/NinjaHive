using NinjaHive.WebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using NinjaHive.Contract;
using NinjaHive.Core.Validation.Attributes;

namespace NinjaHive.WebApp.Extensions
{
    public static class FormExtensions
    {
        public static MvcHtmlString ValidationResultsFor<TValidatable>(this HtmlHelper<TValidatable> htmlHelper, TValidatable model)
            where TValidatable : IValidatable
        {
            var properties =
                from property in model.GetType().GetProperties()
                where Attribute.IsDefined(property, typeof(RequiredForValidationAttribute))
                select property;

            return BuildHtmlMarkupWithStringBuilder(builder =>
            {
                foreach (var property in properties)
                {
                    builder.Append(htmlHelper.Hidden(property.Name, property.GetValue(model)));
                }

                builder.Append(htmlHelper.Partial(Partials.Validation, model));
            });
        }

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
        
        public static MvcHtmlString FormGroupFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object editorHtmlAttributes = null, object containerHtmlAttributes = null)
        {
            var containerAttributes = containerHtmlAttributes ?? new { @class = "form-group" };
            var editorAttributes = editorHtmlAttributes ?? new { @class = "form-control" };

            return BuildHtmlMarkupWithStringBuilder(builder =>
            {
                builder.AppendLine(htmlHelper.BeginElement("div", containerAttributes));
                builder.AppendLine(htmlHelper.LabelFor(expression, new { @class = "col-md-4 control-label" }));
                builder.AppendLine("<div class='col-md-8'>");
                builder.AppendLine(htmlHelper.EditorFor(expression, new { htmlAttributes = editorAttributes }));
                builder.AppendLine(htmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
                builder.AppendLine("</div></div>");
            });
        }

        public static MvcHtmlString FormGroupFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, object editorHtmlAttributes = null, object containerHtmlAttributes = null)
        {
            var containerAttributes = containerHtmlAttributes ?? new { @class = "form-group" };
            var editorAttributes = editorHtmlAttributes ?? new {};

            return BuildHtmlMarkupWithStringBuilder(builder =>
            {
                builder.AppendLine(htmlHelper.BeginElement("div", containerAttributes));
                builder.AppendLine(htmlHelper.EditorFor(expression, new { htmlAttributes = editorAttributes }));
                builder.AppendLine(htmlHelper.LabelFor(expression, new { @class = "control-label" }));
                builder.AppendLine("</div>");
            });
        }

        public static MvcHtmlString FormGroupFor<TModel,TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> list,
            object editorHtmlAttributes = null, object containerHtmlAttributes = null)
        {
            var containerAttributes = containerHtmlAttributes ?? new { @class = "form-group" };
            var editorAttributes = editorHtmlAttributes ?? new { @class = "form-control"};

            return BuildHtmlMarkupWithStringBuilder(builder =>
            {
                builder.AppendLine(htmlHelper.BeginElement("div", containerAttributes));
                builder.AppendLine(htmlHelper.LabelFor(expression, new { @class = "col-md-4 control-label" }));
                builder.AppendLine("<div class='col-md-8'>");

                builder.AppendLine(htmlHelper.DropDownListFor(expression, list, editorAttributes));
                builder.AppendLine(htmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
                builder.AppendLine("</div></div>");
            });
        }

        private static MvcHtmlString BuildHtmlMarkupWithStringBuilder(Action<StringBuilder> markupBuildAction)
        {
            var builder = new StringBuilder();

            markupBuildAction(builder);

            return new MvcHtmlString(builder.ToString());
        }

        private static void AppendLine(this StringBuilder builder, MvcHtmlString htmlString)
        {
            builder.AppendLine(htmlString.ToHtmlString());
        }
    }
}