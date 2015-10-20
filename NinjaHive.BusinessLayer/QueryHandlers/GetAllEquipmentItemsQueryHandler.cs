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
        private readonly IRepository<EquipmentItemEntity> equipmentItemRepository; 
        private readonly IEntityMapper<EquipmentItemEntity, EquipmentItem> itemMapper;

        public GetAllEquipmentItemsQueryHandler(
            IRepository<EquipmentItemEntity> equipmentItemRepository,
            IEntityMapper<EquipmentItemEntity, EquipmentItem> itemMapper)
        {
            this.itemMapper = itemMapper;
            this.equipmentItemRepository = equipmentItemRepository;
        }

        public EquipmentItem[] Handle(GetAllEquipmentItemsQuery query)
        {
            var equipmentItems = this.GetEquipmentItems();

            return this.MapEquipmentItems(equipmentItems);
        }

        private IEnumerable<EquipmentItemEntity> GetEquipmentItems()
        {
            return this.equipmentItemRepository.Entities.ToArray(); //load into memory here
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
