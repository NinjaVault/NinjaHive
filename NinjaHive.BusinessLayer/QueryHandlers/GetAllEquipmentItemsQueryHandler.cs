using System.Collections.Generic;
using System.Linq;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetAllEquipmentItemsQueryHandler
        : IQueryHandler<GetAllEquipmentItemsQuery, EquipmentItem[]>
    {
        private readonly NinjaHiveContext db;

        public GetAllEquipmentItemsQueryHandler(NinjaHiveContext db)
        {
            this.db = db;
        }

        public EquipmentItem[] Handle(GetAllEquipmentItemsQuery query)
        {
            var equipmentItems = this.GetEquipmentItems();

            return this.MapEquipmentItems(equipmentItems).ToArray();
        }

        private IQueryable<EquipmentItemEntity> GetEquipmentItems()
        {
            var equipmentItems = this.db.GameItemEntities.OfType<EquipmentItemEntity>();
            return equipmentItems;
        }

        private IEnumerable<EquipmentItem> MapEquipmentItems(IQueryable<EquipmentItemEntity> equipmentItems)
        {
            return equipmentItems.Select(equipmentItem => new EquipmentItem
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
            });
        }
    }
}
