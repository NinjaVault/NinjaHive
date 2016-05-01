using System.Data.Entity;
using System.Linq;

namespace NinjaHive.Domain.Services
{
    public class NinjaHiveRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly NinjaHiveContext db;

        public NinjaHiveRepository(NinjaHiveContext db)
        {
            this.db = db;
        }

        public IQueryable<TEntity> Entities => this.DbSet;

        public void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }

        private DbSet<TEntity> DbSet => this.db.Set<TEntity>();
    }
}
