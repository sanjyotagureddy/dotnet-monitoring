using Monitoring.Application.Dtos;
using Monitoring.Common.CQRS;

namespace Monitoring.Application.Products.Commands.AddProduct
{
  public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;


  public record CreateProductResult(Guid Id);
}