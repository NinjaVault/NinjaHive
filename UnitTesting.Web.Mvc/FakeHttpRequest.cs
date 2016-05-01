//http://offroadcoder.com/unit-testing-asp-net-html-helpers-with-simple-fakes/
using System;
using System.Collections.Specialized;
using System.Web;

namespace UnitTesting.Web.Mvc
{
	public class FakeHttpRequest : HttpRequestBase
	{
		// Fields
		private readonly string _appRelativeUrl;
		private readonly HttpCookieCollection _cookies;
		private readonly NameValueCollection _formParams;
		private readonly string _httpMethod;
		private readonly bool _isAuthenticated = false;
		private readonly NameValueCollection _queryStringParams;
		private readonly Uri _url;

		// Methods
		public FakeHttpRequest(Uri url, string appRelativeUrl, string httpMethod, bool isAuthenticated, NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies)
		{
			if (!(string.IsNullOrEmpty(appRelativeUrl) || appRelativeUrl.StartsWith("~")))
			{
				throw new Exception("appRelativeUrl must start with ~");
			}
			this._url = url;
			this._appRelativeUrl = appRelativeUrl;
			this._httpMethod = httpMethod;
			this._isAuthenticated = isAuthenticated;
			this._formParams = formParams;
			this._queryStringParams = queryStringParams;
			this._cookies = cookies;
		}

		// Properties
		public override string AppRelativeCurrentExecutionFilePath
		{
			get
			{
				return this._appRelativeUrl;
			}
		}

		public override HttpCookieCollection Cookies
		{
			get
			{
				return this._cookies;
			}
		}

		public override NameValueCollection Form
		{
			get
			{
				return this._formParams;
			}
		}

		public override string HttpMethod
		{
			get
			{
				return this._httpMethod;
			}
		}

		public override bool IsAuthenticated
		{
			get
			{
				return this._isAuthenticated;
			}
		}

		public override bool IsLocal
		{
			get
			{
				return ((this.Url.Host.ToLower() == "localhost") || (this.Url.Host == "127.0.01"));
			}
		}

		public override string PathInfo
		{
			get
			{
				return String.Empty;
			}
		}

		public override NameValueCollection QueryString
		{
			get
			{
				return this._queryStringParams;
			}
		}

		public override Uri Url
		{
			get
			{
				return this._url;
			}
		}
	}
}
