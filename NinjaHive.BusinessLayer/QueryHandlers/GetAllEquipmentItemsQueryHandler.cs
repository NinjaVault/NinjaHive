using System;
using System.Collections;
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
            var equipmentItems = this.db.GameItemEntities.ToArray();
            ArrayList result = new ArrayList();



            for (int i = 0; i < equipmentItems.ToArray().Length; i++)
            {
                if (equipmentItems[i].GetType().BaseType == typeof(EquipmentItemEntity))                
                {
                    var equipmentItem = (EquipmentItemEntity)equipmentItems.ElementAt(i);
                   
                    result.Add(new EquipmentItem
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

            EquipmentItem[] array = result.Cast<EquipmentItem>().ToArray<EquipmentItem>();

            return array;
        }
    }
}
