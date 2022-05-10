using Microsoft.EntityFrameworkCore;
using SM.Domain.CategoryAgg;
using SM.Domain.ImageAgg;
using SM.Domain.OrderAgg;
using SM.Domain.ProductAgg;
using SM.Domain.SlideAgg;
using SM.Infrastructure.EFCore.Mapper;

namespace SM.Infrastructure.EFCore
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }


        #region DbSets

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Order> Orders { get; set; }

        #endregion


        #region Mapper

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CategoryMap).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}