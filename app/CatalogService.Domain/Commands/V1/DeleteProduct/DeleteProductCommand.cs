using MediatR;

namespace CatalogService.Domain.Commands.V1.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
}