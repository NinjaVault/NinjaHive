﻿using System.Linq;

namespace NinjaHive.Domain
{
    public class NinjaHiveRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly NinjaHiveContext db;

        public NinjaHiveRepository(NinjaHiveContext db)
        {
            this.db = db;
        }

        public IQueryable<TEntity> Entities
        {
            get { return this.db.Set<TEntity>(); }
        }
        public void Add(TEntity entity)
        {
            this.db.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            this.db.Set<TEntity>().Remove(entity);
        }
    }
}
