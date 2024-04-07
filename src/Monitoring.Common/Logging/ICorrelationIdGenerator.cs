namespace Monitoring.Common.Logging;

public interface ICorrelationIdGenerator
{
    string Get();
    void Set(string correlationId);
}