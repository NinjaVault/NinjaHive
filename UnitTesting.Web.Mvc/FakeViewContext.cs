//http://offroadcoder.com/unit-testing-asp-net-html-helpers-with-simple-fakes/
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace UnitTesting.Web.Mvc
{
	public class FakeViewContext : ViewContext
	{
		public StringBuilder WriterOutput { get; set; }
		public TextWriter TextWriter { get; set; }

		public FakeViewContext(ControllerContext controllerContext, string viewName, RouteData routeData)
		{
			WriterOutput = new StringBuilder();

			this.Controller = controllerContext.Controller;
			this.View = new RazorView(controllerContext, viewName, "Layout", false, new string[] { });
			this.ViewData = new ViewDataDictionary();
			this.TempData = new TempDataDictionary();
			this.Writer = new StringWriter(WriterOutput);
			this.RouteData = routeData;
		}
	}
}
