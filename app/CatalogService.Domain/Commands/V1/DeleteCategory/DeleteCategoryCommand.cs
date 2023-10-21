using MediatR;

namespace CatalogService.Domain.Commands.V1.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public string Name { get; set; }
}