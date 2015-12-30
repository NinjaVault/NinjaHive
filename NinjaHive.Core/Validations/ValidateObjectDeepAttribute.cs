using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Core.Validations
{
    /// <summary>
    /// This attribute will let a validator perform deep validation on the properties of the object.
    /// It will provide more in-depth details about why a Property is not valid.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateObjectDeepAttribute : ValidationAttribute
    {
        static string memberKey = "members";

        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                // Prevent infinite recursion by keeping track of what we've hit so far
                if (validationContext.Items.ContainsKey(value))
                    return ValidationResult.Success;
                validationContext.Items.Add(value, true);


                Collection<ValidationResult> outResults = new Collection<ValidationResult>();
                // Try validation again, but keep our Items list
                Validator.TryValidateObject(value, new ValidationContext(value, validationContext.Items),
                    outResults, validateAllProperties: true);

                if(outResults.Count > 0)
                {
                    ValidationCumulativeResults validationResult = new ValidationCumulativeResults(
                        string.Format("The field {0} in {1} is not valid.",
                            validationContext.MemberName,
                            validationContext.ObjectType.Name),
                        outResults);
                    return validationResult;
                }

                return ValidationResult.Success;
            }
            catch (ValidationException exception)
            {
                throw new ValidationException(string.Format("Invalid usage of ValidateObjectDeep on member '{0}' of '{1}'",
                    validationContext.MemberName, validationContext.ObjectType.FullName), exception);
            }
        }

        public override bool IsValid(object value)
        {
            var validationContext = new ValidationContext(value);

            if (!validationContext.ObjectType.IsClass)
            {
                throw new ValidationException(string.Format(
                    "Usage of ValidateObjectDeep attribute on a non-class type: {0}", validationContext.DisplayName));
            }

            var validationResults = new Collection<ValidationResult>();
            return Validator.TryValidateObject(value, validationContext, validationResults, validateAllProperties: true);
        }
    }
}
