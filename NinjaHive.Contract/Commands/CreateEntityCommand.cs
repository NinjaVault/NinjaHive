using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Validation.Attributes;

namespace NinjaHive.Contract.Commands
{
    public class CreateEntityCommand<TModel>
        where TModel : class
    {
        public CreateEntityCommand(TModel model)
        {
            this.Model = model;
        }

        [Required]
        [ValidateObjectDeep]
        public TModel Model { get; private set; }
    }
}
