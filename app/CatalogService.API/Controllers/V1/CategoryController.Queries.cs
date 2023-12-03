using CatalogService.Domain.Queries.V1.GetAllCategories;
using CatalogService.Domain.Queries.V1.GetCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers.V1;

public partial class CategoryController
{
    [HttpGet("{name}")]
    [Authorize("BuyerPolicy")]
    public async Task<IActionResult> GetAsync(string name, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetCategoryQuery()
        {
            Name = name
        }, cancellationToken);

        if (result == null)
            return NoContent();

        return Ok(result);
    }

    [HttpGet]
    [Authorize("BuyerPolicy")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken);

        if (!result.Any())
            return NoContent();

        return Ok(result);
    }
}