using System;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Core.Validations
{
    /// <summary>
    /// Use on Guids to validate that the Guid may not be empty.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NonEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var validationResult = base.IsValid(value, validationContext);
                
                if (validationResult == null)
                {
                    return ValidationResult.Success;
                }

                validationResult.ErrorMessage = string.Format("The field '{0}' in '{1}' is invalid!",
                    validationContext.MemberName, validationContext.ObjectType.FullName);
                return validationResult;
            }
            catch (ValidationException exception)
            {
                throw new ValidationException(string.Format("Invalid usage of NonEmptyGuid attribute on member '{0}' of '{1}'",
                    validationContext.MemberName, validationContext.ObjectType.FullName), exception);
            }
        }

        public override bool IsValid(object value)
        {
            if (value is Guid)
            {
                var guid = (Guid) value;

                if (guid.Equals(Guid.Empty))
                {
                    return false;
                }

                return true;
            }

            throw new ValidationException("Usage of NonEmptyGuid attribute should only be used on type of 'Guid'");
        }
    }
}
