using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries;
using NinjaHive.Contract.Extensions;
using NinjaHive.Core;
using NinjaHive.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetSubCategoriesQueryHandler
        : IQueryHandler<GetSubCategoriesQuery, SubCategoryModel[]>
    {
        private readonly IRepository<SubCategoryEntity> repository;
        private readonly IEntityMapper<SubCategoryEntity, SubCategoryModel> subCategoryMapper;



        public GetSubCategoriesQueryHandler(
            IRepository<SubCategoryEntity> repository,
            IEntityMapper<SubCategoryEntity, SubCategoryModel> subCategoryMapper)
        {
            this.repository = repository;
            this.subCategoryMapper = subCategoryMapper;
        }



        public SubCategoryModel[] Handle(GetSubCategoriesQuery query)
        {
            if (query == null)
                return GetAllSubCategories();

            Expression fullExpression = Expression.Constant(true);
            var entityParam = Expression.Parameter(typeof(SubCategoryEntity), "entity");


            if (query.Id != Guid.Empty)
            {
                fullExpression = Expression.And( fullExpression,
                                        CreateTruthExpression<SubCategoryEntity>(category => category.Id == query.Id, entityParam)
                                    );
            }

            if (query.ParentId != null)
            {
                fullExpression = Expression.And(fullExpression,
                    CreateTruthExpression<SubCategoryEntity>(
                        category => category.MainCategory.Id == query.ParentId,
                        entityParam)
                    );
            }
            else if (query.ParentName != null)
            {
                fullExpression = Expression.And(fullExpression,
                    CreateTruthExpression<SubCategoryEntity>(
                        category => category.MainCategory.Name.Contains(query.ParentName),
                        entityParam)
                    );
            }

            if (query.Name != null)
            {
                fullExpression = Expression.And(fullExpression,
                                        CreateTruthExpression<SubCategoryEntity>(category => category.Name.Contains(query.Name), entityParam)
                                    );
            }

            return this.GetSubCategories(Expression.Lambda<Func<SubCategoryEntity, bool>>(fullExpression, new[] { entityParam }));
        }


        private SubCategoryModel[] GetAllSubCategories()
        {
            var categories = from category in this.repository.Entities.ToArray()
                             orderby category.Name, category.MainCategory.Name
                             select this.subCategoryMapper.Map(category);

            return categories.ToArray();
        }



        private SubCategoryModel[] GetSubCategories(Expression<Func<SubCategoryEntity, bool>> truthExpression)
        {
            var queryable = this.repository.Entities.AsQueryable();

            var whereCall = Expression.Call(typeof(Queryable),
                                                "Where",
                                                new[] { queryable.ElementType },
                                                queryable.Expression,
                                                truthExpression);


            var categories = queryable.Provider.CreateQuery<SubCategoryEntity>(whereCall)
                                                .ToArray()
                                                .OrderBy(c => c.Name)
                                                .Select(c => this.subCategoryMapper.Map(c));

            return categories.ToArray();

        }

        protected Expression CreateTruthExpression<T>(Expression<Func<T, bool>> function, params ParameterExpression[] replacementParams)
        {
            return function.StripParameters(replacementParams);
        }
    }
}
