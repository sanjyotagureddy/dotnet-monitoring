using FluentValidation;
using Monitoring.Common.CQRS;

namespace Monitoring.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid OrderId)
  : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
  public DeleteProductCommandValidator()
  {
    RuleFor(x => x.OrderId).NotEmpty().WithMessage("ProductId is required");
  }
}