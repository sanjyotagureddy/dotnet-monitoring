using Monitoring.Application.Interfaces;
using Monitoring.Common.CQRS;
using Monitoring.Common.Exceptions;
using Monitoring.Domain.ValueObjects;

namespace Monitoring.Application.Products.Commands.DeleteProduct;

public class DeleteProductHandler(IApplicationDbContext dbContext)
  : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
  public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
  {
    //Delete Order entity from command object
    //save to database
    //return result

    var orderId = ProductId.Of(command.OrderId);
    var order = await dbContext.Products
      .FindAsync([orderId], cancellationToken: cancellationToken) ?? throw new ProductNotFoundException(command.OrderId);
    dbContext.Products.Remove(order);
    await dbContext.SaveChangesAsync(cancellationToken);

    return new DeleteProductResult(true);
  }
}
