using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.CrossCuttingConcerns
{
    public class SaveChangesCommandHandlerDecorator<TCommand>
        : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decoratee;
        private readonly NinjaHiveEntities dbContext;

        public SaveChangesCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee,
            NinjaHiveEntities dbContext)
        {
            this.decoratee = decoratee;
            this.dbContext = dbContext;
        }

        public void Handle(TCommand command)
        {
            this.decoratee.Handle(command);
            this.dbContext.SaveChanges();
        }
    }
}
