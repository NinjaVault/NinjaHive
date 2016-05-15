using System;
using System.Linq;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.GameItems;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers.GameItems
{
    public class GetAllGameItemsQueryHandler :
        IQueryHandler<GetAllGameItemsQuery<EquipmentModel>, EquipmentModel[]>,
        IQueryHandler<GetAllGameItemsQuery<OtherItemModel>, OtherItemModel[]>,
        IQueryHandler<GetAllGameItemsQuery<SkillItemModel>, SkillItemModel[]>
    {
        private readonly IRepository<EquipmentItemEntity> equipmentItemsRepository;
        private readonly IEntityMapper<EquipmentItemEntity, EquipmentModel> equipmentItemMapper;

        private readonly IRepository<OtherItemEntity> otherItemsRepository;
        private readonly IEntityMapper<OtherItemEntity, OtherItemModel> otherItemsMapper;

        public GetAllGameItemsQueryHandler(
            IRepository<EquipmentItemEntity> equipmentItemsRepository,
            IEntityMapper<EquipmentItemEntity, EquipmentModel> equipmentItemMapper,
            IRepository<OtherItemEntity> otherItemsRepository,
            IEntityMapper<OtherItemEntity, OtherItemModel> otherItemsMapper)
        {
            this.equipmentItemsRepository = equipmentItemsRepository;
            this.equipmentItemMapper = equipmentItemMapper;

            this.otherItemsRepository = otherItemsRepository;
            this.otherItemsMapper = otherItemsMapper;
        }

        public EquipmentModel[] Handle(GetAllGameItemsQuery<EquipmentModel> query)
        {
            var items =
                from item in this.equipmentItemsRepository.Entities.ToArray()
                orderby item.Name ascending
                select this.equipmentItemMapper.Map(item);

            return items.ToArray();
        }


        public SkillItemModel[] Handle(GetAllGameItemsQuery<SkillItemModel> query)
        {
            throw new NotImplementedException();
        }

        public OtherItemModel[] Handle(GetAllGameItemsQuery<OtherItemModel> query)
        {
            var items =
                from item in this.otherItemsRepository.Entities.ToArray()
                orderby item.Name ascending
                select this.otherItemsMapper.Map(item);

            return items.ToArray();
        }
    }
}
