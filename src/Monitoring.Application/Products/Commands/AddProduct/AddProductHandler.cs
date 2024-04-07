using Microsoft.Extensions.Logging;
using Monitoring.Application.Interfaces;
using Monitoring.Common.CQRS;
using Monitoring.Domain.ValueObjects;

namespace Monitoring.Application.Products.Commands.AddProduct
{
  public class AddProductHandler(ILogger<AddProductHandler> logger, IApplicationDbContext repository)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
  {
    private readonly ILogger<AddProductHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IApplicationDbContext _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    #region Implementation of IRequestHandler<in CreateProductCommand, Product>

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
      var product = CreateNewProduct(request);

      await _repository.Products.AddAsync(product, cancellationToken);
      await _repository.SaveChangesAsync(cancellationToken);

      _logger.LogInformation($"Product {product.Id} is successfully created");

      return new CreateProductResult(product.Id.Value);
    }

    private Product CreateNewProduct(CreateProductCommand request)
    {
      var product = new Product
      {
        Id = ProductId.Of(Guid.NewGuid()),
        Name = request.Product.Name,
        Description = request.Product.Description,
        Price = request.Product.Price,
        CreatedAt = DateTime.UtcNow
      };

      return product;
    }

    #endregion
  }
}