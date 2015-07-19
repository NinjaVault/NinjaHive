using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaHive.BusinessLayer.CrossCuttingConcerns;
using NinjaHive.Core;
using NinjaHive.Core.Validations;

namespace NinjaHive.WebApp.Tests
{
    [TestClass]
    public class ValidationDecoratorTests
    {
        [TestMethod]
        public void ValidateObject_ObjectHasRequiredMember_ValidationSucceeds()
        {
            // Arrange
            var validationCommandHandlerDecorator =
                new ValidationCommandHandlerDecorator<IFakeCommandInterface>(new FakeDecoratee());
            var mockupCommand = new MockupRequiredCommand {RequiredProperty = "Required"};

            // Act
            validationCommandHandlerDecorator.Handle(mockupCommand);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ValidateObject_ObjectMissesRequiredMember_ValidationThrowsError()
        {
            // Arrange
            var validationCommandHandlerDecorator =
                new ValidationCommandHandlerDecorator<IFakeCommandInterface>(new FakeDecoratee());
            var mockupCommand = new MockupRequiredCommand {RequiredProperty = null};
            Exception exception = null;

            // Act
            try
            {
                validationCommandHandlerDecorator.Handle(mockupCommand);
            }
            catch(Exception ex)
            {
                exception = ex;
            }
            
            // Assert
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception is ValidationException);
        }

        [TestMethod]
        public void ValidateObjectWithComplexProperty_ComplexMemberHasRequiredMember_ValidationSucceeds()
        {
            // Arrange
            var validationCommandHandlerDecorator =
                new ValidationCommandHandlerDecorator<IFakeCommandInterface>(new FakeDecoratee());
            var mockupRequiredCommand = new MockupRequiredCommand {RequiredProperty = "Required"};
            var mockupComplexCommand = new MockupComplexCommand {ComplexProperty = mockupRequiredCommand};

            // Act
            validationCommandHandlerDecorator.Handle(mockupComplexCommand);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ValidateObjectWithComplexProperty_ComplexMemberMissesRequiredMember_ValidationThrowsError()
        {
            // Arrange
            var validationCommandHandlerDecorator =
                new ValidationCommandHandlerDecorator<IFakeCommandInterface>(new FakeDecoratee());
            var mockupRequiredCommand = new MockupRequiredCommand { RequiredProperty = null };
            var mockupComplexCommand = new MockupComplexCommand { ComplexProperty = mockupRequiredCommand };
            Exception exception = null;

            // Act
            try
            {
                validationCommandHandlerDecorator.Handle(mockupComplexCommand);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception is ValidationException);
        }

        #region Mockups
        private interface IFakeCommandInterface {}
        private class FakeDecoratee : ICommandHandler<IFakeCommandInterface>
        {
            public void Handle(IFakeCommandInterface command)
            {
            }
        }
        private class MockupComplexCommand : IFakeCommandInterface
        {
            [ValidateObject]
            public IFakeCommandInterface ComplexProperty { get; set; }
        }
        private class MockupRequiredCommand : IFakeCommandInterface
        {
            [Required]
            public string RequiredProperty { get; set; }
        }
        #endregion
    }
}
