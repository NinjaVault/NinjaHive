using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaHive.BusinessLayer.CrossCuttingConcerns;
using NinjaHive.Core;
using NinjaHive.Core.Validation;
using NinjaHive.Core.Validation.Attributes;
using NinjaHive.WebApp.Tests.Exceptions;
using NinjaHive.WebApp.Tests.Mocks.Core;

namespace NinjaHive.WebApp.Tests
{
    [TestClass]
    public class ValidationDecoratorTests
    {
        [TestMethod]
        public void ValidateObject_ObjectHasRequiredMember_ValidationSucceeds()
        {
            // Arrange
            var validationCommandHandlerDecorator = this.ValidationDecorator;
            var mockupCommand = new MockupRequiredCommand {RequiredProperty = "Required"};

            // Act
            validationCommandHandlerDecorator.Handle(mockupCommand);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateObject_ObjectMissesRequiredMember_ValidationThrowsError()
        {
            // Arrange
            var validationCommandHandlerDecorator = this.ValidationDecorator;
            var mockupCommand = new MockupRequiredCommand {RequiredProperty = null};

            // Act
            validationCommandHandlerDecorator.Handle(mockupCommand);
        }

        [TestMethod]
        public void ValidateObjectWithComplexProperty_ComplexMemberHasRequiredMember_ValidationSucceeds()
        {
            // Arrange
            var validationCommandHandlerDecorator = this.ValidationDecorator;
            var mockupRequiredCommand = new MockupRequiredCommand {RequiredProperty = "Required"};
            var mockupComplexCommand = new MockupComplexCommand {ComplexProperty = mockupRequiredCommand};

            // Act
            validationCommandHandlerDecorator.Handle(mockupComplexCommand);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ValidationException), "RequiredProperty field is required", MatchSubstring = true)]
        public void ValidateObjectWithComplexProperty_ComplexMemberMissesRequiredMember_ValidationThrowsError()
        {
            // Arrange
            var validationCommandHandlerDecorator = this.ValidationDecorator;
            var mockupRequiredCommand = new MockupRequiredCommand { RequiredProperty = null };
            var mockupComplexCommand = new MockupComplexCommand { ComplexProperty = mockupRequiredCommand };

            // Act
            validationCommandHandlerDecorator.Handle(mockupComplexCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateObject_OnPrimitiveType_ValidationThrowsError()
        {
            // Arrange
            var validationCommandHandlerDecorator = this.ValidationDecorator;
            var mockupPrimitiveTypeCommand = new MockupPrimitiveTypeCommand { NotAnObject = 0 };

            // Act
            validationCommandHandlerDecorator.Handle(mockupPrimitiveTypeCommand);
        }

        [TestMethod]
        public void NonEmptyGuid_GuidIsNotEmpty_Validates()
        {
            // Arrange
            var validationCommandHandlerDecorator = this.ValidationDecorator;
            var mockupGuidCommand = new MockupGuidCommand { Id = this.Guid };

            // Act
            validationCommandHandlerDecorator.Handle(mockupGuidCommand);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void NonEmptyGuid_GuidIsEmpty_ValidationFails()
        {
            // Arrange
            var validationCommandHandlerDecorator = this.ValidationDecorator;
            var mockupGuidCommand = new MockupGuidCommand();

            // Act
            validationCommandHandlerDecorator.Handle(mockupGuidCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void NonEmptyGuid_AttributeUsageOnNonGuidType_ValidationFails()
        {
            // Arrange
            var validationCommandHandlerDecorator = this.ValidationDecorator;
            var mockupGuidCommand = new MockupNonGuidCommand();

            // Act
            validationCommandHandlerDecorator.Handle(mockupGuidCommand);
        }

        private ValidationCommandHandlerDecorator<IFakeCommandInterface> ValidationDecorator
            => new ValidationCommandHandlerDecorator<IFakeCommandInterface>(
                new FakeDecoratee(), FakeServices.GetFakeValidator<IFakeCommandInterface>(null), new ObjectValidator());

        private Guid Guid
        {
            get { return new Guid("BD552713-CC86-4F7E-A5DB-977D9817ECD1"); }
        }

        #region Mockups
        public interface IFakeCommandInterface {}
        private class FakeDecoratee : ICommandHandler<IFakeCommandInterface>
        {
            public void Handle(IFakeCommandInterface command)
            {
            }
        }
        private class MockupComplexCommand : IFakeCommandInterface
        {
            [ValidateObjectDeep]
            public IFakeCommandInterface ComplexProperty { get; set; }
        }
        private class MockupRequiredCommand : IFakeCommandInterface
        {
            [Required]
            public string RequiredProperty { get; set; }
        }
        private class MockupPrimitiveTypeCommand : IFakeCommandInterface
        {
            [ValidateObject]
            public int NotAnObject { get; set; }
        }

        private class MockupGuidCommand : IFakeCommandInterface
        {
            [NonEmptyGuid]
            public Guid Id { get; set; }
        }
        private class MockupNonGuidCommand : IFakeCommandInterface
        {
            [NonEmptyGuid]
            public object Id { get; set; }
        }
        #endregion
    }
}
