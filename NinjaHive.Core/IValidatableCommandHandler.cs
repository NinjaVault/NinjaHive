using System;
using NinjaHive.Core.Exceptions;

namespace NinjaHive.Core
{
    public interface IValidatableCommandHandler<TCommand>
    {
        void Handle(TCommand command, Action<ValidationErrorException> validationErrorAction);
    }
}
