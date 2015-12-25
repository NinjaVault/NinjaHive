using System;
using NinjaHive.Core.Validations;

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
