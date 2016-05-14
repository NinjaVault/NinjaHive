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

        public GetAllGameItemsQueryHandler(
            IRepository<EquipmentItemEntity> equipmentItemsRepository,
            IEntityMapper<EquipmentItemEntity, EquipmentModel> equipmentItemMapper)
        {
            this.equipmentItemsRepository = equipmentItemsRepository;
            this.equipmentItemMapper = equipmentItemMapper;
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
            return new[]
            {
                new OtherItemModel { Id = Guid.Parse("12356458-4568-7895-5568-123645215468"), Name="First Other Item", Description = "The first item of the Other section", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Attack" },
                new OtherItemModel { Id = Guid.NewGuid(), Name="Second Other Item", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Attack" },
                new OtherItemModel { Id = Guid.NewGuid(), Name="Third Other Item", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Defense"},
                new OtherItemModel { Id = Guid.NewGuid(), Name="Foruth Other Item", SubCategoryMainCategoryName="Usables", SubCategoryName="Consumables"},
                new OtherItemModel { Id = Guid.NewGuid(), Name="Fifth Other Item", SubCategoryMainCategoryName="Usables", SubCategoryName="Attack Items"},
            };
        }
    }
}
