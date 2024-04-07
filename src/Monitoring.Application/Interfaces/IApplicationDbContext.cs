using Microsoft.EntityFrameworkCore;

namespace Monitoring.Application.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Domain.Models.Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
