using System.ComponentModel.DataAnnotations;
using System.Linq;
using Moq;
using NinjaHive.Core;

namespace NinjaHive.WebApp.Tests.Mocks.Core
{
    public static class FakeServices
    {
        public static IValidator<T> GetFakeValidator<T>(T command)
        {
            var fakeValidator = new Mock<IValidator<T>>();
            fakeValidator.Setup(v => v.Validate(command)).Returns(Enumerable.Empty<ValidationResult>());
            return fakeValidator.Object;
        }
    }
}
