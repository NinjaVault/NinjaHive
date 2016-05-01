using System;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.GameItems;
using NinjaHive.Core;

namespace NinjaHive.BusinessLayer.QueryHandlers.GameItems
{
    public class GetAllGameItemsQueryHandler :
        IQueryHandler<GetAllItemsQuery<EquipmentModel>, EquipmentModel[]>,
        IQueryHandler<GetAllItemsQuery<OtherItemModel>, OtherItemModel[]>,
        IQueryHandler<GetAllItemsQuery<SkillItemModel>, SkillItemModel[]>
    {
        public SkillItemModel[] Handle(GetAllItemsQuery<SkillItemModel> query)
        {
            throw new NotImplementedException();
        }

        public OtherItemModel[] Handle(GetAllItemsQuery<OtherItemModel> query)
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

        public EquipmentModel[] Handle(GetAllItemsQuery<EquipmentModel> query)
        {
            return new[]
            {
                new EquipmentModel { Id=new Guid("45216fda-6549-5532-432f-afd65a8a7899"), Name = "Sword 01", SubCategoryName="Swords", SubCategoryMainCategoryName="Weapons", Description="The first sword in the set.", Value=50 },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Sword 02", SubCategoryName="Swords", SubCategoryMainCategoryName="Weapons" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Sword 03", SubCategoryName="Swords", SubCategoryMainCategoryName="Weapons" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Axe 01", SubCategoryName="Axe", SubCategoryMainCategoryName="Weapons" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Axe 02", SubCategoryName="Axe", SubCategoryMainCategoryName="Weapons" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Hammer 01", SubCategoryName="Hammer", SubCategoryMainCategoryName="Weapons" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Helm 01", SubCategoryName="Helmets", SubCategoryMainCategoryName="Armor" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Helm 02", SubCategoryName="Helmets", SubCategoryMainCategoryName="Armor" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Torso 01", SubCategoryName="Torsos", SubCategoryMainCategoryName="Armor" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Leg 01", SubCategoryName="Trousers", SubCategoryMainCategoryName="Armor" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Torso 02", SubCategoryName="Torsos", SubCategoryMainCategoryName="Armor" },
            };
        }
    }
}
