using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NinjaHive.Core.Models
{
    public class WorkResult
    {
        public WorkResult()
        {
            this.ValidationResults = new ValidationResult[] { };
        }

        public WorkResult(IEnumerable<ValidationResult> validationResults)
        {
            this.ValidationResults = validationResults;
        }

        public bool IsValid => !this.ValidationResults.Any();
        public IEnumerable<ValidationResult> ValidationResults { get; }
    }
}
