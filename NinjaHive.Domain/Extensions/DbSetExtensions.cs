using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace NinjaHive.Domain.Extensions
{
    public static class DbSetExtensions
    {
        public static T GetById<T>(this DbSet<T> collection, Guid id)
            where T : class, IEntity
        {
            return (T)((DbSet)collection).GetById(id);
        }

        public static object GetById(this DbSet collection, Guid id)
        {
            object entity;

            try
            {
                entity = collection.Find(id);
            }
            catch(Exception exception)
            {
                throw new InvalidOperationException(
                    string.Format("There was an error retrieving an {0} with id {1}. {2}", collection.ElementType.Name,
                        id, exception.Message), exception);
            }

            if (entity == null)
            {
                throw new KeyNotFoundException(string.Format("{0} with id {1} was not found.",
                    collection.ElementType.Name, id));
            }

            return entity;
        }
    }
}
