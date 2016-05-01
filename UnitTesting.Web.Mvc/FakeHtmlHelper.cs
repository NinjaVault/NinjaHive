//http://offroadcoder.com/unit-testing-asp-net-html-helpers-with-simple-fakes/
using System.Web.Mvc;
using System.Web.Routing;

namespace UnitTesting.Web.Mvc
{
	public class FakeHtmlHelper<TModel> : HtmlHelper<TModel>
	{
		public FakeHtmlHelper(ControllerContext controllerContext)
			: this(controllerContext, "index", new RouteData())
		{
		}

		public FakeHtmlHelper(ControllerContext controllerContext, RouteData routeData)
			: this(controllerContext, "index", routeData)
		{
		}

		public FakeHtmlHelper(ControllerContext controllerContext, string viewName, RouteData routeData)
			: base(new FakeViewContext(controllerContext, viewName, routeData), new FakeViewDataContainer())
		{
		}
	}
}
