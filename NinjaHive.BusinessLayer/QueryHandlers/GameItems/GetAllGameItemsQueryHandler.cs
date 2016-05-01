using System.Linq;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.GameItems;
using NinjaHive.Core;
using NinjaHive.Domain;
using System;

namespace NinjaHive.BusinessLayer.QueryHandlers.GameItems
{
    public class GetAllGameItemsQueryHandler :
        IQueryHandler<GetAllItemsQuery<EquipmentModel>, EquipmentModel[]>,
        IQueryHandler<GetAllItemsQuery<OtherItemModel>, OtherItemModel[]>,
        IQueryHandler<GetAllItemsQuery<SkillItemModel>, SkillItemModel[]>
    {
        private readonly IRepository<EquipmentItemEntity> equipmentItemRepository;
        private readonly IRepository<OtherItemEntity> otherItemRepository;
        private readonly IRepository<SkillItemEntity> skillItemRepository;

        private readonly IEntityMapper<EquipmentItemEntity, EquipmentModel> equipmentItemMapper;
        private readonly IEntityMapper<OtherItemEntity, OtherItemModel> otherItemMapper;
        private readonly IEntityMapper<SkillItemEntity, SkillItemModel> skillItemMapper;

        public GetAllGameItemsQueryHandler(IRepository<EquipmentItemEntity> equipmentItemRepository,
            IRepository<OtherItemEntity> otherItemRepository,
            IRepository<SkillItemEntity> skillItemRepository,
            IEntityMapper<EquipmentItemEntity, EquipmentModel> equipmentItemMapper,
            IEntityMapper<OtherItemEntity, OtherItemModel> otherItemMapper,
            IEntityMapper<SkillItemEntity, SkillItemModel> skillItemMapper)
        {
            this.equipmentItemRepository = equipmentItemRepository;
            this.otherItemRepository = otherItemRepository;
            this.skillItemRepository = skillItemRepository;

            this.equipmentItemMapper = equipmentItemMapper;
            this.otherItemMapper = otherItemMapper;
            this.skillItemMapper = skillItemMapper;
        }

        public SkillItemModel[] Handle(GetAllItemsQuery<SkillItemModel> query)
        {
            var skillItems =
                from skillItem in this.skillItemRepository.Entities.ToArray() //load into memory
                orderby skillItem.Name
                select this.skillItemMapper.Map(skillItem);

            return skillItems.ToArray();
        }

        public OtherItemModel[] Handle(GetAllItemsQuery<OtherItemModel> query)
        {
            var others =
                from other in this.otherItemRepository.Entities.ToArray() //load into memory
                orderby other.Name
                select this.otherItemMapper.Map(other);

            return others.ToArray();

            //return new[]
            //{
            //    new OtherItemModel { Id = Guid.Parse("12356458-4568-7895-5568-123645215468"), Name="First Other Item", Description = "The first item of the Other section", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Attack" },
            //    new OtherItemModel { Id = Guid.NewGuid(), Name="Second Other Item", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Attack" },
            //    new OtherItemModel { Id = Guid.NewGuid(), Name="Third Other Item", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Defense"},
            //    new OtherItemModel { Id = Guid.NewGuid(), Name="Foruth Other Item", SubCategoryMainCategoryName="Usables", SubCategoryName="Consumables"},
            //    new OtherItemModel { Id = Guid.NewGuid(), Name="Fifth Other Item", SubCategoryMainCategoryName="Usables", SubCategoryName="Attack Items"},
            //};
        }

        public EquipmentModel[] Handle(GetAllItemsQuery<EquipmentModel> query)
        {
            var equips =
                from equip in this.equipmentItemRepository.Entities.ToArray() //load into memory
                orderby equip.Name
                select this.equipmentItemMapper.Map(equip);

            return equips.ToArray();

            //return new[]
            //{
            //    new EquipmentModel { Id=new Guid("45216fda-6549-5532-432f-afd65a8a7899"), Name = "Sword 01", SubCategoryName="Swords", SubCategoryMainCategoryName="Weapons", Description="The first sword in the set.", Value=50 },
            //    new EquipmentModel { Id=Guid.NewGuid(), Name = "Sword 02", SubCategoryName="Swords", SubCategoryMainCategoryName="Weapons" },
            //    new EquipmentModel { Id=Guid.NewGuid(), Name = "Sword 03", SubCategoryName="Swords", SubCategoryMainCategoryName="Weapons" },
            //    new EquipmentModel { Id=Guid.NewGuid(), Name = "Axe 01", SubCategoryName="Axe", SubCategoryMainCategoryName="Weapons" },
            //    new EquipmentModel { Id=Guid.NewGuid(), Name = "Axe 02", SubCategoryName="Axe", SubCategoryMainCategoryName="Weapons" },
            //    new EquipmentModel { Id=Guid.NewGuid(), Name = "Hammer 01", SubCategoryName="Hammer", SubCategoryMainCategoryName="Weapons" },
            //    new EquipmentModel { Id=Guid.NewGuid(), Name = "Helm 01", SubCategoryName="Helmets", SubCategoryMainCategoryName="Armor" },
            //    new EquipmentModel { Id=Guid.NewGuid(), Name = "Helm 02", SubCategoryName="Helmets", SubCategoryMainCategoryName="Armor" },
            //    new EquipmentModel { Id=Guid.NewGuid(), Name = "Torso 01", SubCategoryName="Torsos", SubCategoryMainCategoryName="Armor" },
            //    new EquipmentModel { Id=Guid.NewGuid(), Name = "Leg 01", SubCategoryName="Trousers", SubCategoryMainCategoryName="Armor" },
            //    new EquipmentModel { Id=Guid.NewGuid(), Name = "Torso 02", SubCategoryName="Torsos", SubCategoryMainCategoryName="Armor" },
            //};
        }
    }
}
