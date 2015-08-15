using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Core.Validations
{
    /// <summary>
    /// This attribute will let a validator perform validation on the properties of the object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validationResult = base.IsValid(value, validationContext);
            
            return validationResult ?? ValidationResult.Success;
        }

        public override bool IsValid(object value)
        {
            var validationContext = new ValidationContext(value);

            var validationResults = new Collection<ValidationResult>();
            return Validator.TryValidateObject(value, validationContext, validationResults, validateAllProperties: true);
        }
    }
}
