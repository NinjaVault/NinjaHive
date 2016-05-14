using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NinjaHive.Core
{
    public interface IUnitOfWork<TModel>
        where TModel : class
    {
        TModel GetById(Guid id);
        void Create(TModel model);
        void Update(TModel model);
        WorkResult Delete(Guid id);
    }

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
