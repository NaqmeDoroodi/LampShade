using BM.Domain.CategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BM.Infrastructure.EFCore.Mapper
{
    public class ArticleCategoryMap : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Desc).HasMaxLength(2000);
            builder.Property(x => x.Img).HasMaxLength(500);
            builder.Property(x => x.ImgAlt).HasMaxLength(500);
            builder.Property(x => x.ImgTitle).HasMaxLength(500);
            builder.Property(x => x.Slug).HasMaxLength(600);
            builder.Property(x => x.Keywords).HasMaxLength(100);
            builder.Property(x => x.MetaDesc).HasMaxLength(150);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(1000);

            builder.HasMany(x => x.Articles).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}