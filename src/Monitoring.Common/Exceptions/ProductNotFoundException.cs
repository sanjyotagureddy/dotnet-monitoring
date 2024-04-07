
namespace Monitoring.Common.Exceptions;
public class ProductNotFoundException(Guid id) 
  : NotFoundException("Product", id);
