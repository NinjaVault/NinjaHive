using System;
using NinjaHive.Contract;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;

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

        public void Create(TModel model)
        {
            var command = new CreateEntityCommand<TModel>(model);
            this.createHandler.Handle(command, e => { });
        }

        public void Update(TModel model)
        {
            var command = new UpdateEntityCommand<TModel>(model.Id, model);
            this.updateHandler.Handle(command, e => { });
        }

        public WorkResult Delete(Guid id)
        {
            var command = new DeleteEntityCommand<TModel>(id);

            WorkResult result = null;

            this.deleteHandler.Handle(command,
                validationErrorAction: exception =>
                {
                    result = new WorkResult(exception.ValidationResults);
                });

            return result ?? new WorkResult();
        }
    }

}