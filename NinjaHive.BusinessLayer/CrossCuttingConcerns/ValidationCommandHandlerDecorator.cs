using System.ComponentModel.DataAnnotations;
using NinjaHive.Core;
using NinjaHive.Core.Helpers;
using System.Linq;
using NinjaHive.Core.Exceptions;

namespace NinjaHive.BusinessLayer.CrossCuttingConcerns
{
    public class ValidationCommandHandlerDecorator<TCommand>
        : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decoratee;
        private readonly IValidator<TCommand> validator;
        private readonly IObjectValidator objectValidator;

        public ValidationCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee,
            IValidator<TCommand> validator,
            IObjectValidator objectValidator)
        {
            this.decoratee = decoratee;
            this.validator = validator;
            this.objectValidator = objectValidator;
        }

        public void Handle(TCommand command)
        {
            Requires.IsNotNull(command, nameof(command));

            this.Validate(command);

            this.decoratee.Handle(command);
        }

        private void Validate(TCommand command)
        {
            var objectValidationResults = this.objectValidator.Validate(command);
            var validationResults = this.validator.Validate(command)
                                                  .Where(r => r != ValidationResult.Success)
                                                  .ToList();

            validationResults.AddRange(objectValidationResults);

            if (validationResults.Any())
            {
                throw new ValidationErrorException(validationResults);
            }
        }
    }
}
