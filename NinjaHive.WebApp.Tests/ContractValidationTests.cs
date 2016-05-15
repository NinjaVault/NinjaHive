using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaHive.BusinessLayer.CrossCuttingConcerns;
using NinjaHive.Contract.Models;
using NinjaHive.Core.Exceptions;
using NinjaHive.Core.Validation;
using NinjaHive.WebApp.Tests.Exceptions;
using NinjaHive.WebApp.Tests.Mocks.Contract;
using NinjaHive.WebApp.Tests.Mocks.Core;

namespace NinjaHive.WebApp.Tests
{
    [TestClass]
    public class ContractValidationTests
    {
        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ValidationErrorException), "The field 'SubCategoryId' in 'NinjaHive.Contract.Models.EquipmentModel' is invalid!", MatchSubstring = true)]
        public void PerformValidation_CreateItemWithoutCategory_ThrowsValidationError()
        {
            // Arrange
            var item = new EquipmentModel
            {
                Name = "Valid",
                Description = "Valid description"
            };

            var command = new AddGameItemCommand(item);
            var fakeHandler = new AddGameItemCommandHandler();
            var fakeValidator = FakeServices.GetFakeValidator(command);

            var handler = new ValidationCommandHandlerDecorator<AddGameItemCommand>(fakeHandler, fakeValidator, new ObjectValidator());

            // Act
            handler.Handle(command);
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ValidationErrorException), "Name is required", MatchSubstring = true)]
        public void PerformValidation_GameItemWithEmptyName_ThrowsValidationError()
        {
            // Arrange
            var item = new EquipmentModel
            {
                Name = string.Empty,
                Description = "Valid description",
            };

            var command = new AddGameItemCommand(item);
            var fakeHandler = new AddGameItemCommandHandler();
            var fakeValidator = FakeServices.GetFakeValidator(command);

            var handler = new ValidationCommandHandlerDecorator<AddGameItemCommand>(fakeHandler, fakeValidator, new ObjectValidator());

            // Act
            handler.Handle(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationErrorException))]
        public void PerformValidation_GameItemNameWithInvalidCharacters_ThrowsValidationError()
        {
            // Arrange
            var item = new EquipmentModel
            {
                Name = "1n val!d",
                Description = "Valid description",
            };

            var command = new AddGameItemCommand(item);
            var fakeHandler = new AddGameItemCommandHandler();
            var fakeValidator = FakeServices.GetFakeValidator(command);

            var handler = new ValidationCommandHandlerDecorator<AddGameItemCommand>(fakeHandler, fakeValidator, new ObjectValidator());

            // Act
            handler.Handle(command);
        }
    }
}
