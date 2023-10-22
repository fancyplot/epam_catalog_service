using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Commands.V1.CreateProduct;

public class CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public int CategoryId { get; set; }
    public int Amount { get; set; }
}