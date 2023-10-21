namespace CatalogService.Domain.Models.V1;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int? ParentId { get; set; }
}