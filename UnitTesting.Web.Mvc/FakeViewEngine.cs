//http://offroadcoder.com/unit-testing-asp-net-html-helpers-with-simple-fakes/
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UnitTesting.Web.Mvc
{
	public class FakeViewEngine : IViewEngine
	{
		public List<string> MethodLog = new List<string>();

		public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
		{
			MethodLog.Add(String.Format("FindPartialView(controllerContext, {0}, {1})", partialViewName, useCache.ToString()));
			return new ViewEngineResult(new List<string>());
		}

		public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
		{
			MethodLog.Add(String.Format("FindView(controllerContext, {0}, {1}, {2})", viewName, masterName, useCache.ToString()));
			return new ViewEngineResult(new List<string>());
		}

		public void ReleaseView(ControllerContext controllerContext, IView view)
		{
			MethodLog.Add("ReleaseView(controllerContext, view)");
		}
	}
}
