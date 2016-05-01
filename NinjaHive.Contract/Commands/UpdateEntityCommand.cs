using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Validation.Attributes;

namespace NinjaHive.Contract.Commands
{
    public class UpdateEntityCommand<TModel>
        where TModel : class
    {
        public UpdateEntityCommand(Guid id, TModel model)
        {
            this.Id = id;
            this.Model = model;
        }

        [NonEmptyGuid]
        public Guid Id { get; private set; }

        [Required]
        [ValidateObjectDeep]
        public TModel Model { get; private set; }
    }
}
