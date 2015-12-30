using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaHive.BusinessLayer.CrossCuttingConcerns;
using NinjaHive.Contract.Models;
using NinjaHive.WebApp.Tests.Exceptions;

namespace NinjaHive.WebApp.Tests
{
    [TestClass]
    public class ContractValidationTests
    {
        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ValidationException), "The field 'CategoryId' in 'NinjaHive.Contract.Models.GameItemModel' is invalid!", MatchSubstring = true)]
        public void PerformValidation_CreateItemWithoutCategory_ThrowsValidationError()
        {
            // Arrange
            var item = new GameItemModel
            {
                Name = "Valid",
                Description = "Valid description"
            };
            var command = new Mocks.Contract.AddGameItemCommand(item);

            var fakeHandler = new Mocks.Contract.AddGameItemCommandHandler();
            var handler = new ValidationCommandHandlerDecorator<Mocks.Contract.AddGameItemCommand>(fakeHandler);

            // Act
            handler.Handle(command);
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ValidationException), "Name is required", MatchSubstring = true)]
        public void PerformValidation_GameItemWithEmptyName_ThrowsValidationError()
        {
            // Arrange
            var item = new GameItemModel
            {
                Name = string.Empty,
                Description = "Valid description",
            };
            var command = new Mocks.Contract.AddGameItemCommand(item);

            var fakeHandler = new Mocks.Contract.AddGameItemCommandHandler();
            var handler = new ValidationCommandHandlerDecorator<Mocks.Contract.AddGameItemCommand>(fakeHandler);

            // Act
            handler.Handle(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void PerformValidation_GameItemNameWithInvalidCharacters_ThrowsValidationError()
        {
            // Arrange
            var item = new GameItemModel
            {
                Name = "1n val!d",
                Description = "Valid description",
            };
            var command = new Mocks.Contract.AddGameItemCommand(item);

            var fakeHandler = new Mocks.Contract.AddGameItemCommandHandler();
            var handler = new ValidationCommandHandlerDecorator<Mocks.Contract.AddGameItemCommand>(fakeHandler);

            // Act
            handler.Handle(command);
        }
    }
}
