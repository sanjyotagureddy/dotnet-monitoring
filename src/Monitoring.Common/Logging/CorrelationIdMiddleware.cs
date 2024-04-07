using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Monitoring.Common.Logging;

public class CorrelationIdMiddleware(RequestDelegate next)
{
  private const string CorrelationIdHeader = "X-Correlation-Id";

    public async Task Invoke(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        var correlationId = GetCorrelationId(context, correlationIdGenerator);
        AddCorrelationIdHeader(context, correlationId);
        await next(context);
    }

    private static StringValues GetCorrelationId(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        if (context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
        {
            correlationIdGenerator.Set(correlationId);
            return correlationId;
        }
        else
        {
            return correlationIdGenerator.Get();
        }
    }
    
    private static void AddCorrelationIdHeader(HttpContext context, StringValues correlationId)
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Add(CorrelationIdHeader, new[] {correlationId.ToString()} );
            return Task.CompletedTask;
        });
    }
}