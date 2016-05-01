using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Helpers;

namespace NinjaHive.Core.Validation
{
    public class CompositeValidator<T> : IValidator<T>
    {
        private readonly IEnumerable<IValidator<T>> validators;

        public CompositeValidator(IEnumerable<IValidator<T>> validators)
        {
            this.validators = validators;
        }

        public IEnumerable<ValidationResult> Validate(T instance)
        {
            Requires.IsNotNull(instance, nameof(instance));

            return
                from validator in this.validators
                from result in validator.Validate(instance)
                select result;
        }
    }
}
