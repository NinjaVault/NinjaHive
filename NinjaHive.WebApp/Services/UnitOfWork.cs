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
        private readonly IPromptableCommandHandler<CreateEntityCommand<TModel>> createHandler;
        private readonly IPromptableCommandHandler<UpdateEntityCommand<TModel>> updateHandler;
        private readonly IPromptableCommandHandler<DeleteEntityCommand<TModel>> deleteHandler;

        public UnitOfWork(
            IQueryProcessor queryProcessor,
            IPromptableCommandHandler<CreateEntityCommand<TModel>> createHandler,
            IPromptableCommandHandler<UpdateEntityCommand<TModel>> updateHandler,
            IPromptableCommandHandler<DeleteEntityCommand<TModel>> deleteHandler)
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
            this.createHandler.Handle(command);
        }

        public void Update(TModel model)
        {
            var command = new UpdateEntityCommand<TModel>(model.Id, model);
            this.updateHandler.Handle(command);
        }

        public void Delete(Guid id)
        {
            var command = new DeleteEntityCommand<TModel>(id);
            this.deleteHandler.Handle(command);
        }
    }
}