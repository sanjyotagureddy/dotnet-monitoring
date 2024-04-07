using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monitoring.Application.Interfaces;
using Monitoring.Infrastructure.Interceptors;

namespace Monitoring.Infrastructure;

public static class RegisterInfrastructureServices
{
  public static IServiceCollection AddInfrastructureServices
    (this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("Product");

    // Add services to the container.
    services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
    services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

    services.AddDbContext<ApplicationDbContext>((sp, options) =>
    {
      options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
      options.UseSqlServer(connectionString);
    });

    services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

    return services;
  }
}