//http://offroadcoder.com/unit-testing-asp-net-html-helpers-with-simple-fakes/
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

namespace UnitTesting.Web.Mvc
{
	public class FakeHttpSessionState : HttpSessionStateBase
	{
		// Fields
		private readonly SessionStateItemCollection _sessionItems;

		// Methods
		public FakeHttpSessionState(SessionStateItemCollection sessionItems)
		{
			this._sessionItems = sessionItems;
		}

		public override void Add(string name, object value)
		{
			this._sessionItems[name] = value;
		}

		public override IEnumerator GetEnumerator()
		{
			return this._sessionItems.GetEnumerator();
		}

		public override void Remove(string name)
		{
			this._sessionItems.Remove(name);
		}

		// Properties
		public override int Count
		{
			get
			{
				return this._sessionItems.Count;
			}
		}

		public override object this[string name]
		{
			get
			{
				return this._sessionItems[name];
			}
			set
			{
				this._sessionItems[name] = value;
			}
		}

		public override object this[int index]
		{
			get
			{
				return this._sessionItems[index];
			}
			set
			{
				this._sessionItems[index] = value;
			}
		}

		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this._sessionItems.Keys;
			}
		}
	}
}
