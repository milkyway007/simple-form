using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class UserSectorOptionConfiguration : IEntityTypeConfiguration<UserSectorOption>
    {
        public void Configure(EntityTypeBuilder<UserSectorOption> builder)
        {
            builder.ToTable(Constants.USER_SECTOR_OPTIONS);

            builder.HasKey(x => new { x.UserId, x.SectorOptionId });

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.SectorOptions)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.SectorOption)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.SectorOptionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
