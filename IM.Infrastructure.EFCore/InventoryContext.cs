using IM.Domain.InventoryAgg;
using IM.Infrastructure.EFCore.Mapper;
using Microsoft.EntityFrameworkCore;

namespace IM.Infrastructure.EFCore
{
    public class InventoryContext : DbContext
    {
        public DbSet<Inventory> Inventory { get; set; }

        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
        }

        #region mapper

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InventoryMap).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
