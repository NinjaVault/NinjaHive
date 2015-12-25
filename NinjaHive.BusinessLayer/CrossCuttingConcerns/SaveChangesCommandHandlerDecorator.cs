using System.Data.Entity;
using System.Data.Entity.Validation;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.CrossCuttingConcerns
{
    public class SaveChangesCommandHandlerDecorator<TCommand>
        : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decoratee;
        private readonly IEntityEditHandler entityEditHandler;
        private readonly DbContext databaseContext;

        public SaveChangesCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee,
            IEntityEditHandler entityEditHandler,
            DbContext databaseContext)
        {
            this.decoratee = decoratee;
            this.entityEditHandler = entityEditHandler;
            this.databaseContext = databaseContext;
        }

        public void Handle(TCommand command)
        {
            this.decoratee.Handle(command);

            try
            {
                this.entityEditHandler.AddIdIfNotExist();
                this.entityEditHandler.SaveEditInfo();
                this.databaseContext.SaveChanges();
            }
            catch (DbEntityValidationException validationException)
            {
                var validationResults = this.databaseContext.GetValidationErrors();
                throw new DbEntityValidationException(
                    "The following validation exception has occurred when trying to save changes to the database:\n",
                    validationResults, validationException);
            }
        }
    }
}
