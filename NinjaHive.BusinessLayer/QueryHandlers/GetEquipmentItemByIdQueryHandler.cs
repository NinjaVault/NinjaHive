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
        private readonly IMapper<EquipmentItemEntity, EquipmentItem> itemMapper;

        public GetEquipmentItemByIdQueryHandler(NinjaHiveContext db,
            IMapper<EquipmentItemEntity, EquipmentItem> itemMapper)
        {
            this.db = db;
            this.itemMapper = itemMapper;
        }

        public EquipmentItem Handle(GetEquipmentItemByIdQuery query)
        {
            var item = (EquipmentItemEntity)db.GameItemEntities.Find(query.EquipmentItemId);

            return this.itemMapper.Map(item);
        }
    }
}
