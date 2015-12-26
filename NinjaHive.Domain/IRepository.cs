using System.Linq;

namespace NinjaHive.Domain
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> Entities { get; }
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
