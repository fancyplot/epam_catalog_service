using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CatalogService.Domain.Commands.V1.CreateProduct;
using CatalogService.Domain.Commands.V1.DeleteProduct;
using CatalogService.Domain.Commands.V1.UpdateProduct;
using CatalogService.API.Models.V1;

namespace CatalogService.API.Controllers.V1;

public partial class ProductController
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([Required] string name, [Required] int categoryId, [Required] decimal price, [Required] int amount, string image, string description,  CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _mediator.Send(new CreateProductCommand()
            {
                Name = name,
                Image = image,
                CategoryId = categoryId,
                Price = price,
                Amount = amount,
                Description = description
            }, cancellationToken);

            var location = $"v1/product/{result.Name}";
            return Created(location, new CreatedResult(location, result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteAsync([Required] string name, [Required] int categoryId, CancellationToken cancellationToken = default)
    {
        try
        {
            await _mediator.Send(new DeleteProductCommand()
            {
                Name = name,
                CategoryId = categoryId
            }, cancellationToken);

            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(UpdateProductRequest updateProductRequest, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<UpdateProductCommand>(updateProductRequest), cancellationToken);

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}