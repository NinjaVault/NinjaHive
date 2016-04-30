using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core;

namespace NinjaHive.WebApp.Services
{
    public class PromptableCommandHandler<TCommand> : IPromptableCommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> commandHandler;

        public PromptableCommandHandler(ICommandHandler<TCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        public void Handle(TCommand command)
        {
            this.Handle(command, null);
        }

        public void Handle(TCommand command, Action successAction)
        {
            this.Handle(command, null, null);
        }

        public void Handle(TCommand command, Action successAction, Action<Exception> failureAction)
        {
            try
            {
                this.commandHandler.Handle(command);
                successAction?.Invoke();
            }
            catch(ValidationException exception)
            {
                throw exception;
                //TODO: somehow show validation results to the view..
            }
            catch (Exception exception)
            {
                throw exception;
                //TODO: show error message and logging and execute failureaction
                //failureAction?.Invoke(exception);
            }
        }
    }
}
