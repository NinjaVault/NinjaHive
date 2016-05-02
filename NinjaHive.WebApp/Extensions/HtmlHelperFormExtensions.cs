using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using NinjaHive.Core.Extensions;

namespace NinjaHive.WebApp.Extensions
{
    public static class HtmlHelperFormExtensions
    {
        public static MvcHtmlString FormGroupForEditor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression)
        {
            var propertyName = expression.GetPropertyNameFromExpression();

            var editor = htmlHelper.Editor(propertyName, new { htmlAttributes = new { @class = "form-control" } });
            var label = htmlHelper.GetLabel(propertyName);
            var validation = htmlHelper.GetValidationMessage(propertyName);

            return BuildFormGroupHtmlTags(editor, label, validation);
        }

        public static MvcHtmlString FormGroupForCheckbox<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression)
        {
            var propertyName = expression.GetPropertyNameFromExpression();

            var editor = htmlHelper.Editor(propertyName);
            var label = htmlHelper.Label(propertyName, new { @class = "control-label" });

            return BuildHtmlMarkupWithStringBuilder(builder =>
            {
                builder.AppendLine("<div class='form-group'>");
                builder.AppendLine(editor);
                builder.AppendLine(label);
                builder.AppendLine("</div>");
            });
        }

        public static MvcHtmlString FormGroupForDropDownList<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> list)
        {
            var propertyName = expression.GetPropertyNameFromExpression();

            var editor = htmlHelper.DropDownList(propertyName, list, new { @class = "form-control" });
            var label = htmlHelper.GetLabel(propertyName);
            var validation = htmlHelper.GetValidationMessage(propertyName);

            return BuildFormGroupHtmlTags(editor, label, validation);
        }

        private static MvcHtmlString GetLabel<TModel>(this HtmlHelper<TModel> htmlHelper, string name)
        {
            return htmlHelper.Label(name, new { @class = "col-md-4 control-label" });
        }

        private static MvcHtmlString GetValidationMessage<TModel>(this HtmlHelper<TModel> htmlHelper, string name)
        {
            return htmlHelper.ValidationMessage(name, string.Empty, new { @class = "text-danger" });
        }

        private static MvcHtmlString BuildFormGroupHtmlTags(
            MvcHtmlString editor, MvcHtmlString label, MvcHtmlString validation)
        {
            return BuildHtmlMarkupWithStringBuilder(builder =>
            {
                builder.AppendLine("<div class='form-group'>");
                builder.AppendLine(label);
                builder.AppendLine("<div class='col-md-8'>");
                builder.AppendLine(editor);
                builder.AppendLine(validation);
                builder.AppendLine("</div>");
                builder.AppendLine("</div>");
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
            builder.AppendLine(htmlString);
        }
    }
}