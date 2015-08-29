using System.Collections.Generic;
using System.Linq;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetEquipmentItemByIdQueryHandler 
        : IQueryHandler<GetEquipmentItemByIdQuery, EquipmentItem>
    {
        private readonly NinjaHiveContext db;

        public GetEquipmentItemByIdQueryHandler(NinjaHiveContext db)
        {
            this.db = db;
        }

        public EquipmentItem Handle(GetEquipmentItemByIdQuery query)
        {
            EquipmentItemEntity item = (EquipmentItemEntity)db.GameItemEntities.Find(query.EquipmentItemId);

            var equip = new EquipmentItem
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Category = item.Category,
                Craftable = item.Craftable,
                UpgradeElement = item.IsUpgrader,
                CraftingElement = item.IsCrafter,
                QuestItem = item.IsQuestItem,
                Durability = item.Durability,
                Value = item.Value,
            };

            return equip;
        }
    }
}
