using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.ImageAgg;

namespace SM.Infrastructure.EFCore.Mapper
{
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Img).HasMaxLength(1000).IsRequired();
            builder.Property(c => c.ImgAlt).HasMaxLength(500).IsRequired();
            builder.Property(c => c.ImgTitle).HasMaxLength(500).IsRequired();
            builder.Property(c => c.IsDeleted);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Images)
                .HasForeignKey(c => c.ProductId);
        }
    }
}