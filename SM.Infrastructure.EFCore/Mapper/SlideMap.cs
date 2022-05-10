using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.SlideAgg;

namespace SM.Infrastructure.EFCore.Mapper
{
    public class SlideMap : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slides");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.Img).IsRequired().HasMaxLength(1000);
            builder.Property(c => c.ImgAlt).IsRequired().HasMaxLength(500);
            builder.Property(c => c.ImgTitle).IsRequired().HasMaxLength(500);
            builder.Property(c => c.Heading).IsRequired().HasMaxLength(255);
            builder.Property(c => c.Title).HasMaxLength(255);
            builder.Property(c => c.Text).HasMaxLength(255);
            builder.Property(c => c.BtnText).IsRequired().HasMaxLength(50);
            builder.Property(c => c.IsDeleted);
        }
    }
}
