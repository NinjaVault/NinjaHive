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
    public class GetItemsQueryHandler: IQueryHandler<GetItemsQuery, GameItemModel[]>
    {
        private readonly IRepository<GameItemEntity> repository;
        private readonly IEntityMapper<GameItemEntity, GameItemModel> gameItemMapper;



        public GetItemsQueryHandler(
            IRepository<GameItemEntity> repository,
            IEntityMapper<GameItemEntity, GameItemModel> gameItemMapper)
        {
            this.repository = repository;
            this.gameItemMapper = gameItemMapper;
        }



        public GameItemModel[] Handle(GetItemsQuery query)
        {
            if (query == null)
                return GetAllGameItem();

            Expression fullExpression = Expression.Constant(true);
            var entityParam = Expression.Parameter(typeof(GameItemEntity), "entity");


            if (query.Id != Guid.Empty)
            {
                fullExpression = Expression.And( fullExpression,
                                        CreateTruthExpression<GameItemEntity>(item => item.Id == query.Id, entityParam)
                                    );
            }
            if (query.Name != null)
            {
                fullExpression = Expression.And(fullExpression,
                                        CreateTruthExpression<GameItemEntity>(item => item.Name.Contains(query.Name), entityParam)
                                    );
            }
            if (query.SubCategory != null && query.MainCategory != null)
            {
                fullExpression = Expression.And(fullExpression,
                    CreateTruthExpression<GameItemEntity>(
                        item => ((item.SubCategory.Name == query.SubCategory) && (item.SubCategory.MainCategory.Name == query.MainCategory)),
                        entityParam)
                    );
            }

            return this.GetGameItems(Expression.Lambda<Func<GameItemEntity, bool>>(fullExpression, new[] { entityParam }));
        }


        private GameItemModel[] GetAllGameItem()
        {
            var categories = from category in this.repository.Entities.ToArray()
                                 orderby category.Name
                                 select this.gameItemMapper.Map(category);

            return categories.ToArray();
        }



        private GameItemModel[] GetGameItems(Expression<Func<GameItemEntity, bool>> truthExpression)
        {
            var queryable = this.repository.Entities.AsQueryable();

            var whereCall = Expression.Call(typeof(Queryable),
                                                "Where",
                                                new[] { queryable.ElementType },
                                                queryable.Expression,
                                                truthExpression);


            var categories = queryable.Provider.CreateQuery<GameItemEntity>(whereCall)
                                                .ToArray()
                                                .OrderBy(c => c.Name)
                                                .Select(c => this.gameItemMapper.Map(c));

            return categories.ToArray();

        }

        protected Expression CreateTruthExpression<T>(Expression<Func<T, bool>> function, params ParameterExpression[] replacementParams)
        {
            return function.StripParameters(replacementParams);
        }
    }
}
