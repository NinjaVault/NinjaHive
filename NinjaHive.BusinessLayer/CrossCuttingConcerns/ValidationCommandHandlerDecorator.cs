using System.ComponentModel.DataAnnotations;
using NinjaHive.Core;
using NinjaHive.Core.Helpers;
using System.Linq;
using System.Collections.Generic;
using System;

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
                var validationMessages = this.TranslateToMessages(validationResults);
                throw new ValidationException(validationMessages);
            }
        }

        private string TranslateToMessages(IEnumerable<ValidationResult> validationResults)
        {
            var messages =
                from result in validationResults
                from subResult in this.DissectValidationResult(result)
                select $" - {subResult.ErrorMessage}";

            return string.Join(Environment.NewLine, messages);
        }

        private IEnumerable<ValidationResult> DissectValidationResult(ValidationResult result)
        {
            var dissected = result as IEnumerable<ValidationResult>;

            return dissected != null ? dissected.SelectMany(DissectValidationResult) : new[] { result };
        }
    }
}
