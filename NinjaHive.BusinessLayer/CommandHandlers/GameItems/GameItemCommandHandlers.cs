using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers.GameItems
{
    public abstract class GameItemCRUDCommandHandlers<TModel, TEntity> :
            ICommandHandler<CreateEntityCommand<TModel>>,
            ICommandHandler<UpdateEntityCommand<TModel>>,
            ICommandHandler<DeleteEntityCommand<TModel>>
            where TModel : GameItemModel
            where TEntity : GameItemEntity
    {
        protected readonly IRepository<SubCategoryEntity> categoryRepository;

        public GameItemCRUDCommandHandlers(IRepository<SubCategoryEntity> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public abstract void Handle(CreateEntityCommand<TModel> command);

        public abstract void Handle(UpdateEntityCommand<TModel> command);

        public abstract void Handle(DeleteEntityCommand<TModel> command);

        protected virtual void UpdateItem(TEntity entity, TModel model)
        {
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.SubCategory = this.categoryRepository.FindById(model.SubCategoryId);
            entity.Value = model.Value;
            entity.Craftable = model.Craftable;
            entity.IsQuestItem = model.IsQuestItem;
            entity.IsCrafter = model.IsCrafter;
            entity.IsUpgrader = model.IsUpgrader;
        }
    }
}
