//http://offroadcoder.com/unit-testing-asp-net-html-helpers-with-simple-fakes/
using System.Linq;
using System.Security.Principal;

namespace UnitTesting.Web.Mvc
{
	public class FakePrincipal : IPrincipal
	{
		// Fields
		private readonly IIdentity _identity;
		private readonly string[] _roles;

		// Methods
		public FakePrincipal(IIdentity identity, string[] roles)
		{
			this._identity = identity;
			this._roles = roles;
		}

		public bool IsInRole(string role)
		{
			if (this._roles == null)
			{
				return false;
			}
			return this._roles.Contains<string>(role);
		}

		// Properties
		public IIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}
	}
}
