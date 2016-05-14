using System;
using NinjaHive.Core;
using NinjaHive.Core.Exceptions;

namespace NinjaHive.WebApp.Services
{
    public class ValidatableCommandHandler<TCommand> : IValidatableCommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> commandHandler;

        public ValidatableCommandHandler(ICommandHandler<TCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        public void Handle(TCommand command, Action<ValidationErrorException> validationErrorAction)
        {
            try
            {
                this.commandHandler.Handle(command);
            }
            catch (ValidationErrorException exception)
            {
                validationErrorAction.Invoke(exception);
            }
            catch (Exception exception)
            {
                throw exception;
                //TODO: show error message? and log error
            }
        }
    }
}
