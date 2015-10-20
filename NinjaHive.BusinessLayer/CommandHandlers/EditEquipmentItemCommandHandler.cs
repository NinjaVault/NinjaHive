using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    class EditEquipmentItemCommandHandler : ICommandHandler<EditEquipmentItemCommand>
    {
        private readonly IRepository<EquipmentItemEntity> equipmentItemRepository;

        public EditEquipmentItemCommandHandler(IRepository<EquipmentItemEntity> equipmentItemRepository)
        {
            this.equipmentItemRepository = equipmentItemRepository;
        }

        public void Handle(EditEquipmentItemCommand command)
        {
            var equipmentItem = command.CreateNew
                ? new EquipmentItemEntity()
                : this.equipmentItemRepository.GetById(command.EquipmentItem.Id);

            equipmentItem.Name = command.EquipmentItem.Name;
            equipmentItem.Description = command.EquipmentItem.Description;
            equipmentItem.Category = command.EquipmentItem.Category;
            equipmentItem.Craftable = command.EquipmentItem.Craftable;
            equipmentItem.IsUpgrader = command.EquipmentItem.UpgradeElement;
            equipmentItem.IsCrafter = command.EquipmentItem.CraftingElement;
            equipmentItem.IsQuestItem = command.EquipmentItem.QuestItem;
            equipmentItem.Durability = command.EquipmentItem.Durability;
            equipmentItem.Value = command.EquipmentItem.Value;

            if (command.CreateNew)
            {
                equipmentItem.Id = command.EquipmentItem.Id;
                this.equipmentItemRepository.Add(equipmentItem);
            }
        }
    }
}
