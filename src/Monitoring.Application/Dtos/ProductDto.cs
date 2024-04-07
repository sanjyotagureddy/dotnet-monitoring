namespace Monitoring.Application.Dtos;

public class ProductDto
{
  public string Name { get; set; }
  public double Price { get; set; }
  public string? Description { get; set; }
  public string? ShortDescription { get; set; }
}