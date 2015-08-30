using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class SaveEquipmentItemCommandHandler : ICommandHandler<SaveEquipmentItemCommand>
    {
        private readonly NinjaHiveContext db;

        public SaveEquipmentItemCommandHandler(NinjaHiveContext db)
        {
            this.db = db;
        }

        public void Handle(SaveEquipmentItemCommand command)
        {
            var item = (EquipmentItemEntity)db.GameItemEntities.Find(command.EquipmentItem.Id);

            item.Name = command.EquipmentItem.Name;
            item.Description = command.EquipmentItem.Description;
            item.Category = command.EquipmentItem.Category;
            item.Craftable = command.EquipmentItem.Craftable;
            item.IsUpgrader = command.EquipmentItem.UpgradeElement;
            item.IsCrafter = command.EquipmentItem.CraftingElement;
            item.IsQuestItem = command.EquipmentItem.QuestItem;
            item.Durability = command.EquipmentItem.Durability;
            item.Value = command.EquipmentItem.Value;
        }
    }
}
