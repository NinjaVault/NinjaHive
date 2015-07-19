using System.ComponentModel.DataAnnotations;
using NinjaHive.Core;

namespace NinjaHive.BusinessLayer.CrossCuttingConcerns
{
    public class ValidationCommandHandlerDecorator<TCommand>
        : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decoratee;

        public ValidationCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee)
        {
            this.decoratee = decoratee;
        }

        public void Handle(TCommand command)
        {
            var validationContext = new ValidationContext(command);
            Validator.ValidateObject(command, validationContext, validateAllProperties: true);

            this.decoratee.Handle(command);
        }
    }
}
