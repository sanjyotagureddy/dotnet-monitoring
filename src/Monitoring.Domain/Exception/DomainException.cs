namespace Monitoring.Domain.Exception;

public class DomainException(string message) 
  : System.Exception($"Domain Exception: \"{message}\".");
  