using AM.Domain.AccountAgg;
using AM.Domain.RoleAgg;
using AM.Infrastructure.EFCore.Mapper;
using Microsoft.EntityFrameworkCore;

namespace AM.Infrastructure.EFCore
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }



        #region mapping

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(AccountMap).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
