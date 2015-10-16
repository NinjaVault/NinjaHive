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
        private readonly IEntityMapper<EquipmentItemEntity, EquipmentItem> itemMapper; 

        public GetAllEquipmentItemsQueryHandler(NinjaHiveContext db,
            IEntityMapper<EquipmentItemEntity, EquipmentItem> itemMapper)
        {
            this.db = db;
            this.itemMapper = itemMapper;
        }

        public EquipmentItem[] Handle(GetAllEquipmentItemsQuery query)
        {
            var equipmentItems = this.GetEquipmentItems();

            return this.MapEquipmentItems(equipmentItems);
        }

        private IEnumerable<EquipmentItemEntity> GetEquipmentItems()
        {
            var equipmentItems = this.db.GameItemEntities.OfType<EquipmentItemEntity>();
            return equipmentItems.ToArray(); //load into memory here
        }

        private EquipmentItem[] MapEquipmentItems(IEnumerable<EquipmentItemEntity> equipmentItems)
        {
            var items =
                from item in equipmentItems
                select this.itemMapper.Map(item);

            return items.ToArray();
        }
    }
}
