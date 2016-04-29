using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Core
{
    public interface IValidator<T>
    {
        IEnumerable<ValidationResult> Validate(T instance);
    }
}
