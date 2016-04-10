using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaHive.Core.Extensions;

namespace NinjaHive.WebApp.Tests
{
    [TestClass]
    public class CoreExtensionTests
    {
        [TestMethod]
        public void RemoveAllWhiteSpace_TextHasSpace_TextHasNoSpaces()
        {
            // Arrange
            var role = "Game Designer";

            // Act
            var trimmedRole = role.RemoveAllWhiteSpace();

            // Assert
            Assert.AreEqual(trimmedRole, "GameDesigner");
        }

        [TestMethod]
        public void RemoveAllWhiteSpace_TextHasSpaces_TextHasNoSpaces()
        {
            // Arrange
            var role = "  Game Designer  ";

            // Act
            var trimmedRole = role.RemoveAllWhiteSpace();

            // Assert
            Assert.AreEqual(trimmedRole, "GameDesigner");
        }

        [TestMethod]
        public void RemoveAllWhiteSpace_TextHasSpacesAndTabs_TextHasNoSpacesAndTabs()
        {
            // Arrange
            var role = "Game\t Desi gner  ";

            // Act
            var trimmedRole = role.RemoveAllWhiteSpace();

            // Assert
            Assert.AreEqual(trimmedRole, "GameDesigner");
        }

        [TestMethod]
        public void RemoveAllWhiteSpace_TextHasSpacesTabsAndNewlines_TextHasNoSpacesTabsAndNewlines()
        {
            // Arrange
            var role = " G\t\tame\t   Desi\n\rgn\ne   r  \r";

            // Act
            var trimmedRole = role.RemoveAllWhiteSpace();

            // Assert
            Assert.AreEqual(trimmedRole, "GameDesigner");
        }
    }
}
