using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.CategoryAgg;

namespace SM.Infrastructure.EFCore.Mapper
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Img);
            builder.Property(c => c.Desc);
            builder.Property(c => c.ImgAlt);
            builder.Property(c => c.ImgTitle);
            builder.Property(c => c.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(c => c.MetaDesc).HasMaxLength(150).IsRequired();
            builder.Property(c => c.Slug).HasMaxLength(300).IsRequired();

            builder.HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}