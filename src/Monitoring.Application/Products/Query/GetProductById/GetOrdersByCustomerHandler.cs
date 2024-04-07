using Microsoft.EntityFrameworkCore;
using Monitoring.Application.Interfaces;
using Monitoring.Common.CQRS;
using Monitoring.Domain.ValueObjects;

namespace Monitoring.Application.Products.Query.GetProductById;
public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        // get orders by customer using dbContext
        // return result

        var orders = await dbContext.Products
          .AsNoTracking()
          .FirstOrDefaultAsync(o => o.Id == ProductId.Of(query.ProductId), cancellationToken: cancellationToken);

        return new GetProductByIdResult(orders);        
    }
}
