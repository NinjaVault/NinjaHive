using System;
using System.Data.Entity;

namespace NinjaHive.Domain.Extensions
{
    public static class RepositoryExtensions
    {
        public static TEntity FindById<TEntity>(this IRepository<TEntity> repository, Guid id)
            where TEntity : class, IEntity
        {
            return ((DbSet<TEntity>) repository.Entities).GetById(id);
        }

        public static void RemoveById<TEntity>(this IRepository<TEntity> repository, Guid id)
            where TEntity : class, IEntity
        {
            var entity = repository.FindById(id);
            repository.Remove(entity);
        }
    }
}
