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
    public async Task<IActionResult> GetAllAsync(int? categoryId = null, int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        if (pageNumber != null && pageNumber <= 0)
            return BadRequest("Page number should be more than zero.");

        if (pageNumber != null && (pageSize == null || pageSize <= 0))
            return BadRequest("Page size should be more than zero.");

        var result = await _mediator.Send(new GetAllProductsQuery()
        {
            CategoryId = categoryId,
            PageNumber = pageNumber,
            PageSize = pageSize
        }, cancellationToken);

        if (!result.Any())
            return NoContent();

        return Ok(result);
    }
}