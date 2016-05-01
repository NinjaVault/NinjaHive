//http://offroadcoder.com/unit-testing-asp-net-html-helpers-with-simple-fakes/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;

namespace UnitTesting.Web.Mvc
{
	public class FakeHttpContext : HttpContextBase
	{
		// Fields
		private readonly string _appRelativeUrl;
		private readonly HttpCookieCollection _cookies;
		private readonly NameValueCollection _formParams;
		private readonly string _httpMethod;
		private readonly HttpRequestBase _httpRequest;
		private readonly HttpResponseBase _httpResponse;
		private bool _isAuthenticated;
		private readonly NameValueCollection _queryStringParams;
		private readonly SessionStateItemCollection _sessionItems;
		private readonly Uri _url;
		private readonly IPrincipal _principal;

		// Methods
		public FakeHttpContext()
			: this(null, "~/", "GET", null, null, null, null, null)
		{
			this._principal = new GenericPrincipal(new GenericIdentity("someUser"), null /* roles */);
		}

		public FakeHttpContext(string appRelativeUrl)
			: this(null, appRelativeUrl, "GET", null, null, null, null, null)
		{
		}

		public FakeHttpContext(string appRelativeUrl, string httpMethod)
			: this(null, appRelativeUrl, httpMethod, null, null, null, null, null)
		{
		}

		public FakeHttpContext(Uri url, string appRelativeUrl)
			: this(url, appRelativeUrl, "GET", null, null, null, null, null)
		{
		}

		public FakeHttpContext(Uri url, string appRelativeUrl, IPrincipal principal)
			: this(url, appRelativeUrl, "GET", principal, null, null, null, null)
		{
		}

		public FakeHttpContext(Uri url, string appRelativeUrl, string httpMethod, IPrincipal principal, NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies, SessionStateItemCollection sessionItems)
		{
			this._isAuthenticated = false;
			this._url = url;
			this._appRelativeUrl = appRelativeUrl;
			this._httpMethod = httpMethod;
			this._formParams = formParams;
			this._queryStringParams = queryStringParams;
			this._cookies = cookies;
			this._sessionItems = sessionItems;
			if (principal != null)
			{
				this._principal = principal;
				this._isAuthenticated = principal.Identity.IsAuthenticated;
			}
			this._httpRequest = new FakeHttpRequest(this._url, this._appRelativeUrl, this._httpMethod, this._isAuthenticated, this._formParams, this._queryStringParams, this._cookies);
			this._httpResponse = new FakeHttpResponse();
		}

		// Properties
		public override HttpRequestBase Request
		{
			get
			{
				return this._httpRequest;
			}
		}

		public override HttpResponseBase Response
		{
			get
			{
				return this._httpResponse;
			}
		}

		public override HttpSessionStateBase Session
		{
			get
			{
				return new FakeHttpSessionState(this._sessionItems);
			}
		}

		public override IPrincipal User
		{
			get
			{
				return _principal;
			}
			set
			{
				base.User = value;
			}
		}

		private Dictionary<object, object> _items = new Dictionary<object, object>();

		public override IDictionary Items { get { return _items; } }
	}
}
