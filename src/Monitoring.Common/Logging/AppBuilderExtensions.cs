using Microsoft.AspNetCore.Builder;

namespace Monitoring.Common.Logging;

public static class AppBuilderExtensions
{
    public static IApplicationBuilder AddCorrelationIdMiddleware(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();
}