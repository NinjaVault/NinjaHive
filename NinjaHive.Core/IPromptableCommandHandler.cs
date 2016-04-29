using System;

namespace NinjaHive.Core
{
    public interface IPromptableCommandHandler<TCommand>
    {
        void Handle(TCommand command);
        void Handle(TCommand command, Action successAction);
        void Handle(TCommand command, Action successAction, Action<Exception> failureAction);
    }
}
