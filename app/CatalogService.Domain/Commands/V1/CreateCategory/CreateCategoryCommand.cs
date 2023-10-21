using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Commands.V1.CreateCategory;

public class CreateCategoryCommand : IRequest<Category>
{
    public string Name { get; set; }
    public string Image { get; set; }
    public int? ParentId{ get; set; }
}