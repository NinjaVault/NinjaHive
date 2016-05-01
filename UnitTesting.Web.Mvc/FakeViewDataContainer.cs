//http://offroadcoder.com/unit-testing-asp-net-html-helpers-with-simple-fakes/
using System.Web.Mvc;

namespace UnitTesting.Web.Mvc
{
	public class FakeViewDataContainer : IViewDataContainer
	{
		private ViewDataDictionary _viewData = new ViewDataDictionary();

		public ViewDataDictionary ViewData { get { return _viewData; } set { _viewData = value; } }
	}
}
