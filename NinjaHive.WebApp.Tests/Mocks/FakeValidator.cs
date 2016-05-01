using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NinjaHive.Core;

namespace NinjaHive.WebApp.Tests.Mocks
{
    public class FakeValidator<T> : IValidator<T>
    {
        public IEnumerable<ValidationResult> Validate(T instance)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}
