using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Queries.V1.GetAllProducts;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
{
    public int? CategoryId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}