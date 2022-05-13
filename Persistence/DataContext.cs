using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Configuration;
using Persistence.Interfaces;

namespace Persistence
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SectorOption> SectorOptions { get; set; }
        public DbSet<UserSectorOption> UserSectorOptons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new SectorOptionConfiguration().Configure(builder.Entity<SectorOption>());
            new UserConfiguration().Configure(builder.Entity<User>());
            new UserSectorOptionConfiguration().Configure(builder.Entity<UserSectorOption>());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            foreach (var entity in ChangeTracker.Entries().Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified)))
            {
                if (entity.State == EntityState.Added)
                {
                    ((Entity)entity.Entity).Created = DateTime.UtcNow;
                }

                ((Entity)entity.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
