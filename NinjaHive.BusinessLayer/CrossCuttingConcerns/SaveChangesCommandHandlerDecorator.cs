using NinjaHive.Core;

namespace NinjaHive.BusinessLayer.CrossCuttingConcerns
{
    public class SaveChangesCommandHandlerDecorator<TCommand>
        : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decoratee;

        public SaveChangesCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee)
        {
            this.decoratee = decoratee;
        }

        public void Handle(TCommand command)
        {
            this.decoratee.Handle(command);
        }
    }
}
