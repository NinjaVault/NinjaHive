using System.Collections.Generic;
using System.Linq;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;


namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class GetEquipmentItemByIdCommandHandler
    {
        private readonly NinjaHiveContext db;

        public GetEquipmentItemByIdCommandHandler(NinjaHiveContext db)
        {
            this.db = db;
        }

        public EquipmentItem Handle(GetEquipmentItemByIdCommand command)
        {
            var equipmentItem = (EquipmentItemEntity)db.GameItemEntities.Find(command.EquipmentId);

            return new EquipmentItem
            {
                Id = equipmentItem.Id,
                Name = equipmentItem.Name,
                Category = equipmentItem.Category,
                Description = equipmentItem.Description,
                Craftable = equipmentItem.Craftable,
                UpgradeElement = equipmentItem.IsUpgrader,
                CraftingElement = equipmentItem.IsCrafter,
                QuestItem = equipmentItem.IsQuestItem,
                Value = equipmentItem.Value,
                Durability = equipmentItem.Durability,
            };
        }

        private IQueryable<EquipmentItemEntity> GetEquipmentItems()
        {
            var equipmentItems = this.db.GameItemEntities.OfType<EquipmentItemEntity>();
            return equipmentItems;
        }
    }
}
