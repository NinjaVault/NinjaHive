using System.Linq;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Contract.Extensions;
using System.Linq.Expressions;
using System;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetMainCategoriesQueryHandler
        : IQueryHandler<GetMainCategoriesQuery, MainCategoryModel[]>
    {
        private readonly IRepository<MainCategoryEntity> repository;
        private readonly IEntityMapper<MainCategoryEntity, MainCategoryModel> mainCategoryMapper;



        public GetMainCategoriesQueryHandler(
            IRepository<MainCategoryEntity> repository,
            IEntityMapper<MainCategoryEntity, MainCategoryModel> mainCategoryMapper)
        {
            this.repository = repository;
            this.mainCategoryMapper = mainCategoryMapper;
        }



        public MainCategoryModel[] Handle(GetMainCategoriesQuery query)
        {
            if (query == null)
                return GetAllMainCategories();

            Expression fullExpression = Expression.Constant(true);
            var entityParam = Expression.Parameter(typeof(MainCategoryEntity), "entity");


            if (query.Id != Guid.Empty)
            {
                fullExpression = Expression.And( fullExpression,
                                        CreateTruthExpression<MainCategoryEntity>(c => c.Id == query.Id, entityParam)
                                    );
            }
            if (query.Name != null)
            {
                fullExpression = Expression.And(fullExpression,
                                        CreateTruthExpression<MainCategoryEntity>(c => c.Name.Contains(query.Name), entityParam)
                                    );
            }
            if (query.HasSubCategory != null)
            {
                fullExpression = Expression.And(fullExpression, 
                    CreateTruthExpression<MainCategoryEntity>(
                        category => category.SubCategories.Any(
                                subCategory => subCategory.Name.Contains(query.HasSubCategory)
                            ),
                        entityParam)
                    );
            }

            return this.GetMainCategories(Expression.Lambda<Func<MainCategoryEntity, bool>>(fullExpression, new[] { entityParam }));
        }


        private MainCategoryModel[] GetAllMainCategories()
        {
            var categories = from category in this.repository.Entities.ToArray()
                                 orderby category.Name
                                 select this.mainCategoryMapper.Map(category);

            return categories.ToArray();
        }



        private MainCategoryModel[] GetMainCategories(Expression<Func<MainCategoryEntity, bool>> truthExpression)
        {
            var queryable = this.repository.Entities.AsQueryable();

            var whereCall = Expression.Call(typeof(Queryable),
                                                "Where",
                                                new[] { queryable.ElementType },
                                                queryable.Expression,
                                                truthExpression);


            var categories = queryable.Provider.CreateQuery<MainCategoryEntity>(whereCall)
                                                .ToArray()
                                                .OrderBy(c => c.Name)
                                                .Select(c => this.mainCategoryMapper.Map(c));

            return categories.ToArray();

        }

        protected Expression CreateTruthExpression<T>(Expression<Func<T, bool>> function, params ParameterExpression[] replacementParams)
        {
            return function.StripParameters(replacementParams);
        }
    }
}
