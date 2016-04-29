using NinjaHive.Core;
using NinjaHive.Contract.Queries.GameItems;
using NinjaHive.Contract.Models;
using System;

namespace NinjaHive.BusinessLayer.QueryHandlers.GameItems
{
    public class GetEquipmentItemsQueryHandler : IQueryHandler<GetEquipmentItemsQuery, EquipmentModel[]>
    {
        public EquipmentModel[] Handle(GetEquipmentItemsQuery query)
        {
            return new []
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
