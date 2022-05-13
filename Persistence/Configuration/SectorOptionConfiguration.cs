using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class SectorOptionConfiguration : IEntityTypeConfiguration<SectorOption>
    {
        public void Configure(EntityTypeBuilder<SectorOption> builder)
        {
            builder.ToTable(Constants.SECTOR_OPTIONS);

            builder.Property(x => x.Label).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Level);

            builder
                .HasMany(x => x.Children)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);
        }
    }
}
