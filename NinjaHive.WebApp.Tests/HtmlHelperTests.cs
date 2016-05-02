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
            var viewModel = new EquipmentViewModel(new EquipmentModel(), Enumerable.Empty<CategoryModel>());

            var viewData = new ViewDataDictionary(viewModel);

            var html = MvcHelper.GetHtmlHelper(viewModel, new EquipmentController(null, null), true);

            // Act
            var output = html.FormGroupFor(m => m.Item.Name);

            // Assert
        }
    }
}
