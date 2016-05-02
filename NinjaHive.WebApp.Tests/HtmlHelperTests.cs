using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaHive.Contract.Models;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Extensions;
using NinjaHive.WebApp.Tests.Mocks.Mvc;

namespace NinjaHive.WebApp.Tests
{
    [TestClass]
    public class HtmlHelperTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var viewModel = new EquipmentViewModel(new EquipmentModel(), Enumerable.Empty<CategoryModel>());

            var viewData = new ViewDataDictionary(viewModel);

            var html = MvcHelper.GetHtmlHelper(viewModel, new MyTestController(), true);

            // Act
            var output = html.FormGroupFor(m => m.Item.Name);

            // Arrange
        }
    }

    public class MyTestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
