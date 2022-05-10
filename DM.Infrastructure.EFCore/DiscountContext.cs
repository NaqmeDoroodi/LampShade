using DM.Domain.ColleagueAgg;
using DM.Domain.CustomerAgg;
using DM.Infrastructure.EFCore.Mapper;
using Microsoft.EntityFrameworkCore;

namespace DM.Infrastructure.EFCore
{
    public class DiscountContext : DbContext
    {
        #region DbSets

        public DbSet<CustomerDisc> CustomerDiscounts { get; set; }
        public DbSet<ColleagueDisc> ColleagueDiscounts { get; set; }

        #endregion

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }

        #region Mapper

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CustomerDiscMap).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
