using System.Data.Entity;

namespace NinjaHive.Core.Decoraters
{
    public class SaveChangesCommandHandlerDecorator<TCommand>
        : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decoratee;
        private readonly DbContext dbContext;

        public SaveChangesCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee,
            DbContext dbContext)
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
