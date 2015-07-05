using NinjaHive.Domain.Entities;
using System.Data.Entity;

namespace NinjaHive.Domain
{
    public class NinjaHiveEntities : DbContext
    {
        public NinjaHiveEntities(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new NinjaHiveInitializer());
        }

        public DbSet<ItemEntity> Items { get; set; }
    }
}