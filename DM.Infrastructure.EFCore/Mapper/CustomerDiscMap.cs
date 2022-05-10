using DM.Domain.CustomerAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DM.Infrastructure.EFCore.Mapper
{
    public class CustomerDiscMap : IEntityTypeConfiguration<CustomerDisc>
    {
        public void Configure(EntityTypeBuilder<CustomerDisc> builder)
        {
            builder.ToTable("CustomerDiscount");

            builder.HasKey(x => x.Id);
        }
    }
}
