using System;
using NinjaHive.Core;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Queries
{
    public class GetEntityByIdQuery<TEntity> : IQuery<TEntity>
        where TEntity : class
    {
        public GetEntityByIdQuery(Guid id)
        {
            this.Id = id;
        }

        [NonEmptyGuid]
        public Guid Id { get; set; }
    }
}
