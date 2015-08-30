using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Core.Validations
{
    /// <summary>
    /// This attribute will let a validator perform validation on the members of the object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var validationResult = base.IsValid(value, validationContext);
                return validationResult ?? ValidationResult.Success;
            }
            catch(ValidationException exception)
            {
                throw new ValidationException(string.Format("Invalid usage of ValidateObject on member '{0}' of '{1}'",
                    validationContext.MemberName, validationContext.ObjectType.FullName), exception);
            }
        }

        public override bool IsValid(object value)
        {
            var validationContext = new ValidationContext(value);

            if (!validationContext.ObjectType.IsClass)
            {
                throw new ValidationException(string.Format(
                    "Usage of ValidateObject attribute on a non-class type: {0}", validationContext.DisplayName));
            }

            var validationResults = new Collection<ValidationResult>();
            return Validator.TryValidateObject(value, validationContext, validationResults, validateAllProperties: true);
        }
    }
}
