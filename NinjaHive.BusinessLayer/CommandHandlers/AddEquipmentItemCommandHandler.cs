using System;
using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    class AddEquipmentItemCommandHandler : ICommandHandler<AddEquipmentItemCommand>
    {
        private readonly NinjaHiveContext db;

        public AddEquipmentItemCommandHandler(NinjaHiveContext db)
        {
            this.db = db;
        }

        public void Handle(AddEquipmentItemCommand command)
        {
            var equipItem = new EquipmentItemEntity
            {
                Id = command.EquipmentItem.Id,
                Name = command.EquipmentItem.Name,
                Category = command.EquipmentItem.Category,
                Description = command.EquipmentItem.Description,
                Craftable = command.EquipmentItem.Craftable,
                IsUpgrader = command.EquipmentItem.UpgradeElement,
                IsCrafter = command.EquipmentItem.CraftingElement,
                IsQuestItem = command.EquipmentItem.QuestItem,
                Value = command.EquipmentItem.Value,
                Durability = command.EquipmentItem.Durability,
            };

            this.db.GameItemEntities.Add(equipItem);
        }
    }
}
