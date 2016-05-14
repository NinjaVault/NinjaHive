using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Extensions;

namespace NinjaHive.Core.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public ValidationErrorException(IEnumerable<ValidationResult> validationResults)
            : base (validationResults.TranslateToMessages())
        {
            this.ValidationResults = validationResults;
        }

        public IEnumerable<ValidationResult> ValidationResults { get; }
    }
}
