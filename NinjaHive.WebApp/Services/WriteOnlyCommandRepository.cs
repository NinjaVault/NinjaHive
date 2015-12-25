using System;
using NinjaHive.Contract.Commands;
using NinjaHive.Core;

namespace NinjaHive.WebApp.Services
{
    public class WriteOnlyCommandRepository<TModel>
        : IWriteOnlyRepository<TModel>
        where TModel : class
    {
        private readonly ICommandHandler<CreateEntityCommand<TModel>> createHandler;
        private readonly ICommandHandler<UpdateEntityCommand<TModel>> updateHandler;
        private readonly ICommandHandler<DeleteEntityCommand<TModel>> deleteHandler;

        public WriteOnlyCommandRepository(
            ICommandHandler<CreateEntityCommand<TModel>> createHandler,
            ICommandHandler<UpdateEntityCommand<TModel>> updateHandler,
            ICommandHandler<DeleteEntityCommand<TModel>> deleteHandler)
        {
            this.createHandler = createHandler;
            this.updateHandler = updateHandler;
            this.deleteHandler = deleteHandler;
        }

        public void Create(TModel model)
        {
            var command = new CreateEntityCommand<TModel>(model);
            this.createHandler.Handle(command);
        }

        public void Update(Guid id, TModel model)
        {
            var command = new UpdateEntityCommand<TModel>(id, model);
            this.updateHandler.Handle(command);
        }

        public void Delete(Guid id)
        {
            var command = new DeleteEntityCommand<TModel>(id);
            this.deleteHandler.Handle(command);
        }
    }
}