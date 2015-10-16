using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetEntityByIdQueryHandler<TEntity>
        : IQueryHandler<GetEntityByIdQuery<TEntity>, TEntity>
        where TEntity : class
    {
        private readonly NinjaHiveContext db;
        private readonly IEntityMapper<TEntity> mapper;

        public GetEntityByIdQueryHandler(NinjaHiveContext db,
            IEntityMapper<TEntity> mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public TEntity Handle(GetEntityByIdQuery<TEntity> query)
        {
            var entity = this.db.Set(this.mapper.SourceType).GetById(query.Id);

            return this.mapper.Map(entity);
        }
    }
}
