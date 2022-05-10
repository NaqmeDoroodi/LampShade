using DM.Domain.ColleagueAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DM.Infrastructure.EFCore.Mapper
{
    public class ColleagueDiscMap : IEntityTypeConfiguration<ColleagueDisc>
    {
        public void Configure(EntityTypeBuilder<ColleagueDisc> builder)
        {
            builder.ToTable("ColleagueDiscounts");

            builder.HasKey(x => x.Id);
        }
    }
}
