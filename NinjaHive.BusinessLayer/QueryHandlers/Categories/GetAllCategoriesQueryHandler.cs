using System.Linq;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.Categories;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers.Categories
{
    public class GetAllCategoriesQueryHandler
        : IQueryHandler<GetAllCategoriesQuery, MainCategoryModel[]>
    {
        private readonly IRepository<MainCategoryEntity> repository;
        private readonly IEntityMapper<MainCategoryEntity, MainCategoryModel> mainCategoryMapper;

        public GetAllCategoriesQueryHandler(
            IRepository<MainCategoryEntity> repository,
            IEntityMapper<MainCategoryEntity, MainCategoryModel> mainCategoryMapper)
        {
            this.repository = repository;
            this.mainCategoryMapper = mainCategoryMapper;
        }

        public MainCategoryModel[] Handle(GetAllCategoriesQuery query)
        {
            return this.GetMainCategories();
        }

        private MainCategoryModel[] GetMainCategories()
        {
            var categories =
                from category in this.repository.Entities.ToArray()
                orderby category.Name
                select this.mainCategoryMapper.Map(category);

            return categories.ToArray(); 
        }
    }
}
