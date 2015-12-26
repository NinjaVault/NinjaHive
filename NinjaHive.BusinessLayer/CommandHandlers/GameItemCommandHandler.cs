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
            var item = new GameItemEntity
            {
                Name = command.Model.Name,
                Description = command.Model.Description,
                Category = command.Model.Category,
                Craftable = command.Model.Craftable,
                IsCrafter = command.Model.IsCrafter,
                IsQuestItem = command.Model.IsQuestItem,
                IsUpgrader = command.Model.IsUpgrader,
                Value = command.Model.Value,
            };

            this.itemRepository.Add(item);
        }

        public void Handle(UpdateEntityCommand<GameItemModel> command)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(DeleteEntityCommand<GameItemModel> command)
        {
            throw new System.NotImplementedException();
        }
    }
}
