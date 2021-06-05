using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickRentalHousing.Domains.Entities;

namespace QuickRentalHousing.Domains.EntityTypeConfigurations
{
    public class LotHomeownerEntityTypeConfiguration : IEntityTypeConfiguration<LotHomeowner>
    {
        public void Configure(EntityTypeBuilder<LotHomeowner> builder)
        {
            builder.HasOne(x => x.Lot)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Homeowner)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
