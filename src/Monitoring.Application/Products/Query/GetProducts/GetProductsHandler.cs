using Microsoft.EntityFrameworkCore;
using Monitoring.Application.Interfaces;
using Monitoring.Common.CQRS;
using Monitoring.Common.Pagination;

namespace Monitoring.Application.Products.Query.GetProducts;
public class GetProductsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        // get orders with pagination
        // return result

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Products.LongCountAsync(cancellationToken);

        var orders = await dbContext.Products
                       .OrderBy(o => o.Name)
                       .Skip(pageSize * pageIndex)
                       .Take(pageSize)
                       .ToListAsync(cancellationToken);

        return new GetProductsResult(
            new PaginatedResult<Product>(
                pageIndex,
                pageSize,
                totalCount,
                orders.ToList()));        
    }
}
