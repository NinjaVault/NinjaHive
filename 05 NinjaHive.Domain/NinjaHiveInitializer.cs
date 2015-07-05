using System;
using System.Collections.Generic;
using System.Data.Entity;
using NinjaHive.Domain.Entities;

namespace NinjaHive.Domain
{
    public class NinjaHiveInitializer : DropCreateDatabaseIfModelChanges<NinjaHiveEntities>
    {
        protected override void Seed(NinjaHiveEntities context)
        {
            var items = new List<ItemEntity>
            {
                new ItemEntity {Id = Guid.NewGuid(), Name = "Demo Item",},
            };
            items.ForEach(i => context.Items.Add(i));
            context.SaveChanges();
        }
    }
}
