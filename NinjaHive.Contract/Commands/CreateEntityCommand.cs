using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Validations;

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
        [ValidateObject]
        public TModel Model { get; private set; }
    }
}
