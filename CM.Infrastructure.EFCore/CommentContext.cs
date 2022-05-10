using CM.Domain.CommentAgg;
using CM.Infrastructure.EFCore.Mapper;
using Microsoft.EntityFrameworkCore;

namespace CM.Infrastructure.EFCore
{
    public class CommentContext : DbContext
    {
        public CommentContext(DbContextOptions<CommentContext> options) : base(options)
        {
        }


        public DbSet<Comment> Comments { get; set; }



        #region mapping

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CommentMap).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
