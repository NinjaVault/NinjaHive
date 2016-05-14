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

        [TestMethod]
        public void IsThrownFrom_SimpleClass_ReturnsTrue()
        {
            // Arrange
            var mock = new SimpleClass();

            try
            {
                mock.ThrowException();
            }
            catch (Exception exception)
            {
                // Act
                var result = exception.IsThrownFrom(typeof(SimpleClass));

                // Assert
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void IsThrownFrom_ClassThatImplementsInterface_ReturnsTrue()
        {
            // Arrange
            var mock = new ImplementationClass();

            try
            {
                mock.ThrowException();
            }
            catch(Exception exception)
            {
                // Act
                var result = exception.IsThrownFrom(typeof(ImplementationClass));

                // Assert
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void IsThrownFrom_ClassThatImplementsOpenGenericInterface_ReturnsTrue()
        {
            // Arrange
            var mock = new OpenGenericImplementationClass<object>();

            try
            {
                mock.ThrowException();
            }
            catch (Exception exception)
            {
                // Act
                var result = exception.IsThrownFrom(typeof(OpenGenericImplementationClass<>));

                // Assert
                Assert.IsTrue(result);
            }
        }
    }

    public class SimpleClass
    {
        public void ThrowException()
        {
            throw new Exception();
        }
    }

    public class ImplementationClass : IException
    {
        public void ThrowException()
        {
            throw new Exception();
        }
    }

    public class OpenGenericImplementationClass<T> : IOpenGenericException<T>
    {
        public void ThrowException()
        {
            throw new Exception();
        }
    }

    public interface IException
    {
        void ThrowException();
    }

    public interface IOpenGenericException<T>
    {
        void ThrowException();
    }
}
