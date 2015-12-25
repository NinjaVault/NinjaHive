using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NinjaHive.Core;

namespace NinjaHive.Domain
{
    public class EntityEditHandler : IEntityEditHandler
    {
        private readonly IUserContext userContext;
        private readonly ITimeProvider timeProvider;
        private readonly DbContext databaseContext;

        public EntityEditHandler(
            IUserContext userContext,
            ITimeProvider timeProvider,
            DbContext databaseContext)
        {
            this.userContext = userContext;
            this.timeProvider = timeProvider;
            this.databaseContext = databaseContext;
        }

        public void SaveEditInfo()
        {
            this.UpdateAddedEntities();
            this.UpdateModifiedEntities();
        }

        public void AddIdIfNotExist()
        {
            var addedEntitiesWithNoIds =
                this.GetChangeTrackersEntries(EntityState.Added)
                    .OfType<IEntity>()
                    .Where(entity => entity.Id == Guid.Empty);

            foreach (var entity in addedEntitiesWithNoIds)
            {
                entity.Id = Guid.NewGuid();
            }
        }

        private void UpdateAddedEntities()
        {
            var addedEntities = this.GetChangeTrackersEntries(EntityState.Added);

            foreach (var entity in addedEntities.OfType<IEntity>())
            {
                entity.EditInfo.CreatedOn = entity.EditInfo.EditedOn = this.timeProvider.Now;
                entity.EditInfo.CreatedBy = entity.EditInfo.EditedBy = this.userContext.UserName;
            }
        }

        private void UpdateModifiedEntities()
        {
            var modifiedEntities = this.GetChangeTrackersEntries(EntityState.Modified);

            foreach (var entity in modifiedEntities.OfType<IEntity>())
            {
                entity.EditInfo.EditedOn = this.timeProvider.Now;
                entity.EditInfo.EditedBy = this.userContext.UserName;
            }
        }

        private IEnumerable<object> GetChangeTrackersEntries(EntityState state)
        {
            return
                from entry in this.databaseContext.ChangeTracker.Entries()
                where entry.State == state
                select entry.Entity;
        } 
    }
}
