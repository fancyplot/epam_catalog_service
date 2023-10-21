using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Commands.V1.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Category>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int? ParentId { get; set; }
}