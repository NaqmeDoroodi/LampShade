using BM.Domain.ArticleAgg;
using BM.Domain.CategoryAgg;
using BM.Infrastructure.EFCore.Mapper;
using Microsoft.EntityFrameworkCore;

namespace BM.Infrastructure.EFCore
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        #region dbSets

        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Article> Articles { get; set; }

        #endregion


        #region mapping

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ArticleCategoryMap).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
