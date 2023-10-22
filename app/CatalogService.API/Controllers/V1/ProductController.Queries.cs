using CatalogService.Domain.Queries.V1.GetAllProducts;
using CatalogService.Domain.Queries.V1.GetProduct;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers.V1;

public partial class ProductController
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetAsync(string name, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetProductQuery()
        {
            Name = name
        }, cancellationToken);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);

        if (!result.Any())
            return NoContent();

        return Ok(result);
    }
}