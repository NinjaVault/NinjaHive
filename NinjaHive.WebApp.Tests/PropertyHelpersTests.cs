using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaHive.WebApp.Helpers;

namespace NinjaHive.WebApp.Tests
{
    [TestClass]
    public class PropertyHelpersTests
    {
        [TestMethod]
        public void PropertyHelper_DisplayName()
        {
            var name = PropertyHelper.DisplayNameFromExpression<Mocks.Models.GameItemModel, Guid>( m => m.Id);

            Assert.AreEqual(name, "Identity");
        }


        [TestMethod]
        public void PropertyHelper_DisplayNameNoDisplay()
        {
            var name = PropertyHelper.DisplayNameFromExpression<Mocks.Models.GameItemModel, string>(m => m.Name);

            Assert.AreEqual(name, "Name");
        }

        [TestMethod]
        public void PropertyHelper_RealNameNoDisplay()
        {
            var name = PropertyHelper.NameFromExpression<Mocks.Models.GameItemModel, string>(m => m.Name);

            Assert.AreEqual(name, "Name");
        }


        [TestMethod]
        public void PropertyHelper_RealNameWithDisplay()
        {
            var name = PropertyHelper.NameFromExpression<Mocks.Models.GameItemModel, Guid>(m => m.Id);

            Assert.AreEqual(name, "Id");
        }



        [TestMethod]
        public void PropertyHelper_FieldDisplayName()
        {
            var name = PropertyHelper.DisplayNameFromExpression<Mocks.Models.GameItemModel, string>(m => m.aField);

            Assert.AreEqual(name, "A Field");
        }

        [TestMethod]
        public void PropertyHelper_FieldRealName()
        {
            var name = PropertyHelper.NameFromExpression<Mocks.Models.GameItemModel, string>(m => m.aField);

            Assert.AreEqual(name, "aField");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PropertyHelper_RealNameErrorNotAMember()
        {
            var name = PropertyHelper.NameFromExpression<Mocks.Models.GameItemModel, string>(m => m.Worm());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PropertyHelper_DisplayNameErrorNotAMember()
        {
            var name = PropertyHelper.DisplayNameFromExpression<Mocks.Models.GameItemModel, string>(m => m.Worm());
        }


    }
}
