using MediatR;

using Microsoft.AspNetCore.Mvc;

using Monitoring.Application.Products.Commands.AddProduct;
using Monitoring.Application.Products.Commands.DeleteProduct;
using Monitoring.Application.Products.Query.GetProductById;
using Monitoring.Application.Products.Query.GetProducts;
using Monitoring.Common.Pagination;

namespace Monitoring.Api.Endpoints
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController(IMediator mediator) : ControllerBase
  {
    [HttpPost("getAll")]
    public async Task<IActionResult> GetProducts([AsParameters] PaginationRequest request)
    {
      var query = new GetProductsQuery(request);
      var result = await mediator.Send(query);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
      var query = new GetProductByIdQuery(id);
      var result = await mediator.Send(query);

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {
      var result = await mediator.Send(command);

      return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
    //{
    //  if (id != command.Id)
    //  {
    //    return BadRequest();
    //  }

    //  var result = await _mediator.Send(command);

    //  return Ok(result);
    //}

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
      var command = new DeleteProductCommand(id);
      var result = await mediator.Send(command);

      return Ok(result);
    }
  }
}
