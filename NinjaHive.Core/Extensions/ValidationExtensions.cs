using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NinjaHive.Core.Extensions
{
    public static class ValidationExtensions
    {
        public static string TranslateToMessages(this IEnumerable<ValidationResult> validationResults)
        {
            var messages =
                from result in validationResults
                from subResult in result.DissectValidationResult()
                select $" - {subResult.ErrorMessage}";

            return string.Join(Environment.NewLine, messages);
        }

        private static IEnumerable<ValidationResult> DissectValidationResult(this ValidationResult result)
        {
            var dissected = result as IEnumerable<ValidationResult>;

            return dissected != null ? dissected.SelectMany(DissectValidationResult) : new[] { result };
        }
    }
}
