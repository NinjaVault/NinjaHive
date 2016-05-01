//http://offroadcoder.com/unit-testing-asp-net-html-helpers-with-simple-fakes/
using System;
using System.Security.Principal;

namespace UnitTesting.Web.Mvc
{
	public class FakeIdentity : IIdentity
	{
		// Fields
		private readonly string _name;

		// Methods
		public FakeIdentity(string userName)
		{
			this._name = userName;
		}

		// Properties
		public string AuthenticationType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsAuthenticated
		{
			get
			{
				return !string.IsNullOrEmpty(this._name);
			}
		}

		public string Name
		{
			get
			{
				return this._name;
			}
		}
	}
}
