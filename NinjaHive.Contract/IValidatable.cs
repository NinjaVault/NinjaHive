using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Contract
{
    public interface IValidatable
    {
        ICollection<ValidationResult> ValidationResults { get; }
    }
}
