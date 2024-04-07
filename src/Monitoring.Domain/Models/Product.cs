using Monitoring.Domain.Abstractions;
using Monitoring.Domain.ValueObjects;

namespace Monitoring.Domain.Models;

public class Product : Entity<ProductId>

{
  public string Name { get; set; } = string.Empty;
  public double Price { get; set; }
  public string? Description { get; set; }
  public string? ShortDescription { get; set; }
}