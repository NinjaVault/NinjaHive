using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Helpers;

namespace NinjaHive.Core.Validation
{
    public class ObjectValidator : IObjectValidator
    {
        public IEnumerable<ValidationResult> Validate(object instance)
        {
            instance.IsNotNull(nameof(instance));

            var validationResults = new Collection<ValidationResult>();

            var validationContext = new ValidationContext(instance);

            Validator.TryValidateObject(instance, validationContext, validationResults, validateAllProperties: true);

            return validationResults;
        }
    }
}
