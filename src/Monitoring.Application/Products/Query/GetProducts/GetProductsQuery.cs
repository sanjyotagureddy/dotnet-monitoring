using Monitoring.Common.CQRS;
using Monitoring.Common.Pagination;

namespace Monitoring.Application.Products.Query.GetProducts;

public record GetProductsQuery(PaginationRequest PaginationRequest) 
    : IQuery<GetProductsResult>;

public record GetProductsResult(PaginatedResult<Product> Products);