using System.Reflection;
using Monitoring.Application.Interfaces;
using Monitoring.Domain.Models;

namespace Monitoring.Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
  : DbContext(options), IApplicationDbContext
{
  public DbSet<Product> Products => Set<Product>();

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    base.OnModelCreating(builder);
  }
}
