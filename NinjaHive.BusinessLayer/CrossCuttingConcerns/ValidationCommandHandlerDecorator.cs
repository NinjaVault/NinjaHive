using System.Collections.ObjectModel;
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
            var outResults = new Collection<ValidationResult>();

            var validationContext = new ValidationContext(command);
            Validator.TryValidateObject(command, validationContext, outResults, validateAllProperties: true);

            if(outResults.Count > 0)
            {
                throw new ValidationException(outResults[0].ToString());
            }

            this.decoratee.Handle(command);
        }
    }
}
