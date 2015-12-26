using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, ReadOnlyCollection<CategoryModel>>
    {
        private readonly IRepository<CategoryEntity> repository;
        private readonly IEntityMapper<CategoryEntity, CategoryModel> mapper;

        public GetAllCategoriesQueryHandler(
            IRepository<CategoryEntity> repository,
            IEntityMapper<CategoryEntity, CategoryModel> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public ReadOnlyCollection<CategoryModel> Handle(GetAllCategoriesQuery query)
        {
            var categories = this.MapCategories();
            return new ReadOnlyCollection<CategoryModel>(categories);
        }

        private IList<CategoryModel> MapCategories()
        {
            var categories =
                from category in this.repository.Entities.ToArray() //load into memory
                select this.mapper.Map(category);

            return categories.ToList(); 
        }
    }
}
