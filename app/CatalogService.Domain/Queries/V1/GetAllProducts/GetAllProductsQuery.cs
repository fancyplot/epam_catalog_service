using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Queries.V1.GetAllProducts;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
{
    
}