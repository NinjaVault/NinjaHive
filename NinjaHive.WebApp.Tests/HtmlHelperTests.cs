using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaHive.Contract.Models;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Extensions;
using NinjaHive.Core.Extensions;
using NinjaHive.WebApp.Tests.Mocks.Mvc;
using System;
using System.Linq.Expressions;
using NinjaHive.WebApp.Areas.Items.Controllers;

namespace NinjaHive.WebApp.Tests
{
    [TestClass]
    public class HtmlHelperTests
    {
        [TestMethod]
        public void GetPropertyNameFromExpression_PropertyFromFirstClass_GetsPropertyName()
        {
            // Arrange
            Expression<Func<EquipmentModel, string>> expression = m => m.Name;

            // Act
            var name = expression.GetPropertyNameFromExpression();

            // Assert
            Assert.AreEqual(name, "Name");
        }

        [TestMethod]
        public void GetPropertyNameFromExpression_PropertyFromReferencedClass_GetsPropertyName()
        {
            // Arrange
            Expression<Func<EquipmentViewModel, string>> expression = m => m.Item.Name;

            // Act
            var name = expression.GetPropertyNameFromExpression();

            // Assert
            Assert.AreEqual(name, "Name");
        }

        [TestMethod]
        public void GetPropertyNameFromExpression_ExpressionBodyIsMethodCall_ThrowsArgumentException()
        {
            // Arrange
            Expression<Func<EquipmentController, ActionResult>> expression = c => c.Index();

            var errorMessage = string.Empty;

            // Act
            try
            {
                var name = expression.GetPropertyNameFromExpression();
            }
            catch (ArgumentException exception)
            {
                errorMessage = exception.Message;
            }

            // Assert
            Assert.AreEqual(errorMessage, $"Lambda body '{expression}' is not a MemberExpression");
        }

        [TestMethod]
        public void TestMethod1() //TODO create tests for the FormExtensions
        {
            // Arrange
            var viewModel = new EquipmentViewModel { Item = new EquipmentModel(), CategoriesList = Enumerable.Empty<CategoryModel>() };

            var viewData = new ViewDataDictionary(viewModel);

            var html = MvcHelper.GetHtmlHelper(viewModel, new EquipmentController(null, null), true);

            // Act
            var output = html.FormGroupFor(m => m.Item.Name);

            // Assert
        }

        [TestMethod]
        public void HtmlBuilder_PlainTagStart()
        {
            // Arrange
            var viewModel = new EquipmentViewModel { Item = new EquipmentModel(), CategoriesList = Enumerable.Empty<CategoryModel>() };

            var viewData = new ViewDataDictionary(viewModel);

            var html = MvcHelper.GetHtmlHelper(viewModel, new EquipmentController(null, null), true);

            // Act
            var element = html.BeginElement("div");

            // Assert
            Assert.AreEqual("<div>", element.ToString());
        }


        [TestMethod]
        public void HtmlBuilder_PlainTagStart_SingleAttribute_NoDashesNoQuotes()
        {
            // Arrange
            var viewModel = new EquipmentViewModel { Item = new EquipmentModel(), CategoriesList = Enumerable.Empty<CategoryModel>() };

            var viewData = new ViewDataDictionary(viewModel);

            var html = MvcHelper.GetHtmlHelper(viewModel, new EquipmentController(null, null), true);

            // Act
            var element = html.BeginElement("div", new { @class = "justice" });

            // Assert
            Assert.AreEqual("<div class='justice'>", element.ToString());
        }


        [TestMethod]
        public void HtmlBuilder_PlainTagStart_SingleAttribute_DashesNoQuotes()
        {
            // Arrange
            var viewModel = new EquipmentViewModel { Item = new EquipmentModel(), CategoriesList = Enumerable.Empty<CategoryModel>() };

            var viewData = new ViewDataDictionary(viewModel);

            var html = MvcHelper.GetHtmlHelper(viewModel, new EquipmentController(null, null), true);

            // Act
            var element = html.BeginElement("div", new { @class = "justice", data_model="template" });

            // Assert
            Assert.AreEqual("<div class='justice' data-model='template'>", element.ToString());
        }



        [TestMethod]
        public void HtmlBuilder_PlainTagStart_SingleAttribute_DashesQuotes()
        {
            // Arrange
            var viewModel = new EquipmentViewModel { Item = new EquipmentModel(), CategoriesList = Enumerable.Empty<CategoryModel>() };

            var viewData = new ViewDataDictionary(viewModel);

            var html = MvcHelper.GetHtmlHelper(viewModel, new EquipmentController(null, null), true);

            // Act
            var element = html.BeginElement("div", new { @class = "justice", data_characters_allowed = "\"'" });

            // Assert
            Assert.AreEqual("<div class='justice' data-characters-allowed='\"\\''>", element.ToString());
        }


        [TestMethod]
        public void HtmlBuilder_SelfCloseTagStart()
        {
            // Arrange
            var viewModel = new EquipmentViewModel { Item = new EquipmentModel(), CategoriesList = Enumerable.Empty<CategoryModel>() };

            var viewData = new ViewDataDictionary(viewModel);

            var html = MvcHelper.GetHtmlHelper(viewModel, new EquipmentController(null, null), true);

            // Act
            var element = html.BeginElementSelfClose("input");

            // Assert
            Assert.AreEqual("<input/>", element.ToString());
        }


        [TestMethod]
        public void HtmlBuilder_SelfCloseTagStart_SingleAttribute_NoDashesNoQuotes()
        {
            // Arrange
            var viewModel = new EquipmentViewModel { Item = new EquipmentModel(), CategoriesList = Enumerable.Empty<CategoryModel>() };

            var viewData = new ViewDataDictionary(viewModel);

            var html = MvcHelper.GetHtmlHelper(viewModel, new EquipmentController(null, null), true);

            // Act
            var element = html.BeginElementSelfClose("input", new { @class = "justice" });

            // Assert
            Assert.AreEqual("<input class='justice'/>", element.ToString());
        }


        [TestMethod]
        public void HtmlBuilder_SelfCloseTagStart_SingleAttribute_DashesNoQuotes()
        {
            // Arrange
            var viewModel = new EquipmentViewModel { Item = new EquipmentModel(), CategoriesList = Enumerable.Empty<CategoryModel>() };

            var viewData = new ViewDataDictionary(viewModel);

            var html = MvcHelper.GetHtmlHelper(viewModel, new EquipmentController(null, null), true);

            // Act
            var element = html.BeginElementSelfClose("input", new { @class = "justice", data_model = "template" });

            // Assert
            Assert.AreEqual("<input class='justice' data-model='template'/>", element.ToString());
        }



        [TestMethod]
        public void HtmlBuilder_SelfCloseTagStart_SingleAttribute_DashesQuotes()
        {
            // Arrange
            var viewModel = new EquipmentViewModel { Item = new EquipmentModel(), CategoriesList = Enumerable.Empty<CategoryModel>() };

            var viewData = new ViewDataDictionary(viewModel);

            var html = MvcHelper.GetHtmlHelper(viewModel, new EquipmentController(null, null), true);

            // Act
            var element = html.BeginElementSelfClose("input", new { @class = "justice", data_characters_allowed = "\"'" });

            // Assert
            Assert.AreEqual("<input class='justice' data-characters-allowed='\"\\''/>", element.ToString());
        }
    }
}
