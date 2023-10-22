using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Queries.V1.GetProduct;

public class GetProductQuery : IRequest<IEnumerable<Product>>
{
    public string Name { get; set; }
}