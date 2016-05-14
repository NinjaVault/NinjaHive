using System;
using System.Linq;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.Categories;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers.Categories
{
    public class GetGroupedCategoriesQueryHandler : IQueryHandler<GetGroupedCategoriesQuery, CategoryModel[]>
    {
        private readonly IRepository<SubCategoryEntity> categoriesRepository;
        private readonly IEntityMapper<SubCategoryEntity, CategoryModel> categoriesMapper;

        public GetGroupedCategoriesQueryHandler(
            IRepository<SubCategoryEntity> categoriesRepository,
            IEntityMapper<SubCategoryEntity, CategoryModel> categoriesMapper)
        {
            this.categoriesRepository = categoriesRepository;
            this.categoriesMapper = categoriesMapper;
        }

        public CategoryModel[] Handle(GetGroupedCategoriesQuery query)
        {
            var categories =
                from category in this.categoriesRepository.Entities.ToArray()
                orderby category.MainCategory.Name ascending, category.Name ascending
                select this.categoriesMapper.Map(category);

            return categories.ToArray();
        }
    }
}
