using NinjaHive.Domain.Entities;
using System.Data.Entity;

namespace NinjaHive.Domain
{
    public class NinjaHiveEntities : DbContext
    {
        public NinjaHiveEntities(string connectionstring)
            : base(connectionstring)
        {
            Database.Initialize(true);
        }

        public DbSet<ItemEntity> Items { get; set; }
    }
}