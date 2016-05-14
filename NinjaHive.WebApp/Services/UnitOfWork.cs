using System;
using NinjaHive.Contract;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Core.Models;

namespace NinjaHive.WebApp.Services
{
    public class UnitOfWork<TModel> : IUnitOfWork<TModel>
        where TModel : class, IModel
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IValidatableCommandHandler<CreateEntityCommand<TModel>> createHandler;
        private readonly IValidatableCommandHandler<UpdateEntityCommand<TModel>> updateHandler;
        private readonly IValidatableCommandHandler<DeleteEntityCommand<TModel>> deleteHandler;

        public UnitOfWork(
            IQueryProcessor queryProcessor,
            IValidatableCommandHandler<CreateEntityCommand<TModel>> createHandler,
            IValidatableCommandHandler<UpdateEntityCommand<TModel>> updateHandler,
            IValidatableCommandHandler<DeleteEntityCommand<TModel>> deleteHandler)
        {
            this.queryProcessor = queryProcessor;
            this.createHandler = createHandler;
            this.updateHandler = updateHandler;
            this.deleteHandler = deleteHandler;
        }

        public TModel GetById(Guid id)
        {
            var query = new GetEntityByIdQuery<TModel>(id);
            return this.queryProcessor.Execute(query);
        }

        public WorkResult Create(TModel model)
        {
            var command = new CreateEntityCommand<TModel>(model);
            return this.Handle(this.createHandler, command);
        }

        public WorkResult Update(TModel model)
        {
            var command = new UpdateEntityCommand<TModel>(model.Id, model);
            return this.Handle(this.updateHandler, command);
        }

        public WorkResult Delete(Guid id)
        {
            var command = new DeleteEntityCommand<TModel>(id);
            return this.Handle(this.deleteHandler, command);
        }

        private WorkResult Handle<TCommand>(IValidatableCommandHandler<TCommand> handler, TCommand command)
        {
            WorkResult result = null;

            handler.Handle(command,
                validationErrorAction: exception =>
                {
                    result = new WorkResult(exception.ValidationResults);
                });

            return result ?? new WorkResult();
        }
    }

}