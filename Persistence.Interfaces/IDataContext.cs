using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Interfaces
{
    public interface IDataContext
    {
        DbSet<User> Users { get; set; }
        DbSet<SectorOption> SectorOptions { get; set; }
        DbSet<UserSectorOption> UserSectorOptons { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
