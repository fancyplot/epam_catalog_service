using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Commands.V1.UpdateProduct;

public class UpdateProductCommand : IRequest<Product>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? Image { get; set; }
    public int? CategoryId { get; set; } = null;
    public int? Amount { get; set; }
}