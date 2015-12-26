using System;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    class GameItemCommandHandler :
        ICommandHandler<CreateEntityCommand<GameItemModel>>,
        ICommandHandler<UpdateEntityCommand<GameItemModel>>,
        ICommandHandler<DeleteEntityCommand<GameItemModel>>
    {
        private readonly IRepository<GameItemEntity> itemRepository;

        public GameItemCommandHandler(IRepository<GameItemEntity> itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public void Handle(CreateEntityCommand<GameItemModel> command)
        {
            var entity = new GameItemEntity();
            this.UpdateItem(entity, command.Model);
            this.itemRepository.Add(entity);
        }

        public void Handle(UpdateEntityCommand<GameItemModel> command)
        {
            var entity = this.itemRepository.GetById(command.Id);
            this.UpdateItem(entity, command.Model);
        }

        private void UpdateItem(GameItemEntity entity, GameItemModel model)
        {
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Category = model.Category;
            entity.Value = model.Value;
            entity.Craftable = model.Craftable;
            entity.IsQuestItem = model.IsQuestItem;
            entity.IsCrafter = model.IsCrafter;
            entity.IsUpgrader = model.IsUpgrader;
        }

        public void Handle(DeleteEntityCommand<GameItemModel> command)
        {
            throw new System.NotImplementedException();
        }
    }
}
