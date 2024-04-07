using Monitoring.Common.CQRS;

namespace Monitoring.Application.Products.Query.GetProductById;

public record GetProductByIdQuery(Guid ProductId) 
    : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);
