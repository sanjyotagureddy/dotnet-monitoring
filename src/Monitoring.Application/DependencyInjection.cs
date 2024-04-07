using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

using Monitoring.Common.Behaviors;
using Monitoring.Common.Logging;

namespace Monitoring.Application;
public static class DependencyInjection
{
  public static IServiceCollection AddApplicationServices
      (this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
    services.AddMediatR(config =>
    {
      config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
      config.AddOpenBehavior(typeof(ValidationBehavior<,>));
      config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });


    services.AddFeatureManagement();

    return services;
  }
}
