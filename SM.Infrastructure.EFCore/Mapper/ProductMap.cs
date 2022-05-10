using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.ProductAgg;

namespace SM.Infrastructure.EFCore.Mapper
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Code).HasMaxLength(15).IsRequired();
            builder.Property(c => c.Img);
            builder.Property(c => c.ShortDesc).HasMaxLength(500).IsRequired(); ;
            builder.Property(c => c.Desc);
            builder.Property(c => c.ImgAlt);
            builder.Property(c => c.ImgTitle);
            builder.Property(c => c.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(c => c.MetaDesc).HasMaxLength(150).IsRequired();
            builder.Property(c => c.Slug).HasMaxLength(300).IsRequired();

            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
            builder.HasMany(x => x.Images).WithOne(x => x.Product).HasForeignKey(c => c.ProductId);
        }
    }
}
