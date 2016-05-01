using System.Web.Mvc;
using System.Web.Routing;
using UnitTesting.Web.Mvc;

namespace NinjaHive.WebApp.Tests.Mocks.Mvc
{
    public static class MvcHelper
    {
        public static HtmlHelper<TModel> GetHtmlHelper<TModel>(
            TModel model, ControllerBase controller, bool clientValidationEnabled)
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FakeViewEngine());

            var httpContext = new FakeHttpContext();

            var viewData = new FakeViewDataContainer { ViewData = new ViewDataDictionary<TModel>(model) };

            var routeData = new RouteData();
            routeData.Values["controller"] = "home";
            routeData.Values["action"] = "index";

            ControllerContext controllerContext = new FakeControllerContext(controller);

            var viewContext = new FakeViewContext(controllerContext, "MyView", routeData);
            viewContext.HttpContext = httpContext;
            viewContext.ClientValidationEnabled = clientValidationEnabled;
            viewContext.UnobtrusiveJavaScriptEnabled = clientValidationEnabled;
            viewContext.FormContext = new FakeFormContext();

            HtmlHelper<TModel> htmlHelper = new HtmlHelper<TModel>(viewContext, viewData);
            return htmlHelper;
        }
    }
}
