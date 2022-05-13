using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(Constants.USERS);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.IsAgreed);
        }
    }
}
