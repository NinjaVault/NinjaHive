using System;
using NinjaHive.Core.Validation.Attributes;

namespace NinjaHive.Contract.Commands
{
    public class DeleteEntityCommand<TModel>
        where TModel : class
    {
        public DeleteEntityCommand(Guid id)
        {
            this.Id = id;
        }

        [NonEmptyGuid]
        public Guid Id { get; private set; }
    }
}
