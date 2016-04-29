using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Core
{
    public interface IObjectValidator
    {
        IEnumerable<ValidationResult> Validate(object instance);
    }
}
