//http://offroadcoder.com/unit-testing-asp-net-html-helpers-with-simple-fakes/
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace UnitTesting.Web.Mvc
{
	public class FakeControllerContext : ControllerContext
	{
		public FakeControllerContext(ControllerBase controller)
			: this(controller, new RouteData(), String.Empty, null, null, null, null, null, null)
		{
		}

		public FakeControllerContext(ControllerBase controller, NameValueCollection formParams)
			: this(controller, new RouteData(), String.Empty, null, null, formParams, null, null, null)
		{
		}

		public FakeControllerContext(ControllerBase controller, string userName)
			: this(controller, new RouteData(), String.Empty, userName, null, null, null, null, null)
		{
		}

		public FakeControllerContext(ControllerBase controller, HttpCookieCollection cookies)
			: this(controller, new RouteData(), String.Empty, null, null, null, null, cookies, null)
		{
		}

		public FakeControllerContext(ControllerBase controller, RouteData routeData)
			: this(controller, routeData, String.Empty, null, null, null, null, null, null)
		{
		}

		public FakeControllerContext(ControllerBase controller, SessionStateItemCollection sessionItems)
			: this(controller, new RouteData(), String.Empty, null, null, null, null, null, sessionItems)
		{
		}

		public FakeControllerContext(ControllerBase controller, NameValueCollection formParams, NameValueCollection queryStringParams)
			: this(controller, new RouteData(), String.Empty, null, null, formParams, queryStringParams, null, null)
		{
		}

		public FakeControllerContext(ControllerBase controller, string userName, NameValueCollection formParams)
			: this(controller, new RouteData(), String.Empty, userName, null, formParams, null, null, null)
		{
		}

		public FakeControllerContext(ControllerBase controller, string userName, string[] roles)
			: this(controller, new RouteData(), String.Empty, userName, roles, null, null, null, null)
		{
		}

		public FakeControllerContext(ControllerBase controller, RouteData routeData, string appRelativeUrl, string userName, string[] roles, NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies, SessionStateItemCollection sessionItems)
			: base(new FakeHttpContext(null, appRelativeUrl, "GET", new FakePrincipal(new FakeIdentity(userName), roles), formParams, queryStringParams, cookies, sessionItems), routeData, controller)
		{
		}
	}
}
