using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    class GameItemCommandHandler :
        ICommandHandler<CreateEntityCommand<GameItemModel>>,
        ICommandHandler<UpdateEntityCommand<GameItemModel>>,
        ICommandHandler<DeleteEntityCommand<GameItemModel>>
    {
        private readonly IRepository<GameItemEntity> itemRepository;
        private readonly IRepository<CategoryEntity> categoryRepository; 

        public GameItemCommandHandler(IRepository<GameItemEntity> itemRepository,
            IRepository<CategoryEntity> categoryRepository)
        {
            this.itemRepository = itemRepository;
            this.categoryRepository = categoryRepository;
        }

        public void Handle(CreateEntityCommand<GameItemModel> command)
        {
            var entity = new GameItemEntity();
            this.UpdateItem(entity, command.Model);
            this.itemRepository.Add(entity);
        }

        public void Handle(UpdateEntityCommand<GameItemModel> command)
        {
            var entity = this.itemRepository.FindById(command.Id);
            this.UpdateItem(entity, command.Model);
        }

        private void UpdateItem(GameItemEntity entity, GameItemModel model)
        {
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Category = this.categoryRepository.FindById(model.CategoryId);
            entity.Value = model.Value;
            entity.Craftable = model.Craftable;
            entity.IsQuestItem = model.IsQuestItem;
            entity.IsCrafter = model.IsCrafter;
            entity.IsUpgrader = model.IsUpgrader;
        }

        public void Handle(DeleteEntityCommand<GameItemModel> command)
        {
            this.itemRepository.RemoveById(command.Id);
        }
    }
}
