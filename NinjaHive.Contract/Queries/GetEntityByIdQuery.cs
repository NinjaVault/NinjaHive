using System;
using NinjaHive.Core;

namespace NinjaHive.Contract.Queries
{
    public class GetEntityByIdQuery<TEntity> : IQuery<TEntity>
        where TEntity : class
    {
        public GetEntityByIdQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
