using System.ComponentModel.DataAnnotations;
using CatalogService.Domain.Commands.V1.CreateCategory;
using CatalogService.Domain.Commands.V1.DeleteCategory;
using CatalogService.Domain.Commands.V1.UpdateCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers.V1;

public partial class CategoryController
{
    /// <summary>
    /// Creates a new category
    /// </summary>
    /// <param name="name">The category name</param>
    /// <param name="image">The category image URL</param>
    /// <param name="parenId">The parent category id</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The created category response</returns>
    [HttpPost]
    [Authorize("ManagerPolicy")]
    public async Task<IActionResult> PostAsync([Required] string name, string image, int? parenId, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _mediator.Send(new CreateCategoryCommand()
            {
                Name = name,
                Image = image,
                ParentId = parenId
            }, cancellationToken);

            var location = $"v1/category/{result.Name}";
            return Created(location, new CreatedResult(location, result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{name}")]
    [Authorize("ManagerPolicy")]
    public async Task<IActionResult> DeleteAsync(string name, CancellationToken cancellationToken = default)
    {
        try
        {
            await _mediator.Send(new DeleteCategoryCommand()
            {
                Name = name
            }, cancellationToken);

            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize("ManagerPolicy")]
    public async Task<IActionResult> PutAsync([Required] int id, string name, string image, int? parenId, CancellationToken cancellationToken = default)
    {
        try
        {
            await _mediator.Send(new UpdateCategoryCommand()
            {
                Id = id,
                Name = name,
                Image = image,
                ParentId = parenId
            }, cancellationToken);

            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}