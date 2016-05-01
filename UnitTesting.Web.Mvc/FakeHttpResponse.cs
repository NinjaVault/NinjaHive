//http://offroadcoder.com/unit-testing-asp-net-html-helpers-with-simple-fakes/
using System.IO;
using System.Text;
using System.Web;

namespace UnitTesting.Web.Mvc
{
	public class FakeHttpResponse : HttpResponseBase
	{
		// Fields
		private StringBuilder _sb = new StringBuilder();
		private int _statusCode;
		private StringWriter _sw;

		// Methods
		public FakeHttpResponse()
		{
			this._sw = new StringWriter(this._sb);
		}

		public override void Clear()
		{
			this._sb = new StringBuilder();
			this._sw = new StringWriter();
		}

		public override string ToString()
		{
			return this._sb.ToString();
		}

		public override void Write(string s)
		{
			this._sb.Append(s);
		}

		// Properties
		public override HttpCookieCollection Cookies
		{
			get
			{
				return base.Cookies;
			}
		}

		public override TextWriter Output
		{
			get
			{
				return this._sw;
			}
		}

		public override int StatusCode
		{
			get
			{
				return this._statusCode;
			}
			set
			{
				this._statusCode = value;
			}
		}

		public override string StatusDescription { get; set; }
	}
}
