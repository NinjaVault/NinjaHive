using System;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.Categories;
using NinjaHive.Core;

namespace NinjaHive.BusinessLayer.QueryHandlers.Categories
{
    public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, CategoryModel[]>
    {
        public CategoryModel[] Handle(GetCategoriesQuery query)
        {
            return new[]
            {
                new CategoryModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Swords",
                    MainCategoryName = "Weapons",
                },
                new CategoryModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Pendants",
                    MainCategoryName = "Weapons",
                },
                new CategoryModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Shields",
                    MainCategoryName = "Armors",
                },
            };
        }
    }
}
