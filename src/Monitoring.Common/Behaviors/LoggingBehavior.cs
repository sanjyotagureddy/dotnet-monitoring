using MediatR;

using Microsoft.Extensions.Logging;

using System.Diagnostics;
using Newtonsoft.Json;

namespace Monitoring.Common.Behaviors;
public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
        typeof(TRequest).Name, typeof(TResponse).Name, request);

    var timer = new Stopwatch();
    timer.Start();

    var response = await next();

    timer.Stop();
    var timeTaken = timer.Elapsed;
    if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
      logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
          typeof(TRequest).Name, timeTaken.Seconds);

    logger.LogInformation("[END] Handled {Request} with {Response}  - RequestData ={RequestData} - ReponseDate={ResponseData}", typeof(TRequest).Name, typeof(TResponse).Name, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response));
    logger.LogInformation("RequestData ={RequestData} - ReponseDate={ResponseData}", JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response));
    return response;
  }
}
