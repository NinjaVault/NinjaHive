using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class EquipmentItemCRUDCommandHandlers :
        ICommandHandler<CreateEntityCommand<EquipmentModel>>,
        ICommandHandler<UpdateEntityCommand<EquipmentModel>>,
        ICommandHandler<DeleteEntityCommand<EquipmentModel>>
    {
        private readonly IRepository<EquipmentItemEntity> itemRepository;
        private readonly IRepository<SubCategoryEntity> categoryRepository; 

        public EquipmentItemCRUDCommandHandlers(
            IRepository<EquipmentItemEntity> itemRepository,
            IRepository<SubCategoryEntity> categoryRepository)
        {
            this.itemRepository = itemRepository;
            this.categoryRepository = categoryRepository;
        }

        public void Handle(CreateEntityCommand<EquipmentModel> command)
        {
            var entity = new EquipmentItemEntity();
            this.UpdateItem(entity, command.Model);
            this.itemRepository.Add(entity);
        }

        public void Handle(UpdateEntityCommand<EquipmentModel> command)
        {
            var entity = this.itemRepository.FindById(command.Id);
            this.UpdateItem(entity, command.Model);
        }

        private void UpdateItem(EquipmentItemEntity entity, EquipmentModel model)
        {
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.SubCategory = this.categoryRepository.FindById(model.CategoryId);
            entity.Value = model.Value;
            entity.Craftable = model.Craftable;
            entity.IsQuestItem = model.IsQuestItem;
            entity.IsCrafter = model.IsCrafter;
            entity.IsUpgrader = model.IsUpgrader;
        }

        public void Handle(DeleteEntityCommand<EquipmentModel> command)
        {
            this.itemRepository.RemoveById(command.Id);
        }
    }
}
