using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Queries.V1.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
{
    
}