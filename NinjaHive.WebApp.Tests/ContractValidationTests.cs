using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaHive.BusinessLayer.CrossCuttingConcerns;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Core.Validations;

namespace NinjaHive.WebApp.Tests
{
    [TestClass]
    public class ContractValidationTests
    {
        [TestMethod]
        public void PerformValidation_CreateValidGameItem_ThrowsValidationError()
        {
            // Arrange
            var item = new GameItemModel
            {
                Name = "Valid",
                Description = "Valid description",
            };
            var command = new MockAddGameItemCommand(item);

            var fakeHandler = new MockAddGameItemCommandHandler();
            var handler = new ValidationCommandHandlerDecorator<MockAddGameItemCommand>(fakeHandler);

            // Act
            handler.Handle(command);
        }

        [TestMethod]
        public void PerformValidation_GameItemWithEmptyName_ThrowsValidationError()
        {
            // Arrange
            var item = new GameItemModel
            {
                Name = string.Empty,
                Description = "Valid description",
            };
            var command = new MockAddGameItemCommand(item);

            var fakeHandler = new MockAddGameItemCommandHandler();
            var handler = new ValidationCommandHandlerDecorator<MockAddGameItemCommand>(fakeHandler);

            var failed = false;

            // Act
            try
            {
                handler.Handle(command);
            }
            catch(ValidationException exception)
            {
                failed = true;
            }

            // Assert
            Assert.IsTrue(failed);
        }

        [TestMethod]
        public void PerformValidation_GameItemNameWithInvalidCharacters_ThrowsValidationError()
        {
            // Arrange
            var item = new GameItemModel
            {
                Name = "1n val!d",
                Description = "Valid description",
            };
            var command = new MockAddGameItemCommand(item);

            var fakeHandler = new MockAddGameItemCommandHandler();
            var handler = new ValidationCommandHandlerDecorator<MockAddGameItemCommand>(fakeHandler);

            var failed = false;

            // Act
            try
            {
                handler.Handle(command);
            }
            catch (ValidationException exception)
            {
                failed = true;
            }

            // Assert
            Assert.IsTrue(failed);
        }
    }

    public class MockAddGameItemCommand
    {
        public MockAddGameItemCommand(GameItemModel item)
        {
            this.Item = new CreateEntityCommand<GameItemModel>(item);
        }

        [ValidateObject]
        public CreateEntityCommand<GameItemModel> Item { get; private set; }
    }

    public class MockAddGameItemCommandHandler : ICommandHandler<MockAddGameItemCommand>
    {
        public void Handle(MockAddGameItemCommand command)
        {
        }
    }
}
