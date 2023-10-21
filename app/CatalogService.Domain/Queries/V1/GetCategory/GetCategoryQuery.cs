using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Queries.V1.GetCategory;

public class GetCategoryQuery : IRequest<Category>
{
    public string Name { get; set; }
}