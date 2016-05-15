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

        private readonly IRepository<SkillItemEntity> skillItemsRepository;
        private readonly IEntityMapper<SkillItemEntity, SkillItemModel> skillItemsMapper;

        public GetAllGameItemsQueryHandler(
            IRepository<EquipmentItemEntity> equipmentItemsRepository,
            IEntityMapper<EquipmentItemEntity, EquipmentModel> equipmentItemMapper,
            IRepository<OtherItemEntity> otherItemsRepository,
            IEntityMapper<OtherItemEntity, OtherItemModel> otherItemsMapper,
            IRepository<SkillItemEntity> skillItemsRepository,
            IEntityMapper<SkillItemEntity, SkillItemModel> skillItemsMapper)
        {
            this.equipmentItemsRepository = equipmentItemsRepository;
            this.equipmentItemMapper = equipmentItemMapper;

            this.otherItemsRepository = otherItemsRepository;
            this.otherItemsMapper = otherItemsMapper;

            this.skillItemsRepository = skillItemsRepository;
            this.skillItemsMapper = skillItemsMapper;
        }

        public EquipmentModel[] Handle(GetAllGameItemsQuery<EquipmentModel> query)
        {
            return this.GetMappedItems(this.equipmentItemsRepository.Entities, this.equipmentItemMapper.Map);
        }

        public SkillItemModel[] Handle(GetAllGameItemsQuery<SkillItemModel> query)
        {
            return this.GetMappedItems(this.skillItemsRepository.Entities, this.skillItemsMapper.Map);
        }

        public OtherItemModel[] Handle(GetAllGameItemsQuery<OtherItemModel> query)
        {
            return this.GetMappedItems(this.otherItemsRepository.Entities, this.otherItemsMapper.Map);
        }

        private TModel[] GetMappedItems<TModel, TEntity>(IQueryable<TEntity> entities, Func<TEntity, TModel> mapper)
            where TModel : GameItemModel
            where TEntity : GameItemEntity, INamedEntity
        {
            var items =
                from item in entities.ToArray()
                orderby item.Name ascending
                select mapper.Invoke(item);

            return items.ToArray();
        }
    }
}
