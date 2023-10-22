using System.ComponentModel.DataAnnotations;

namespace CatalogService.API.Models.V1;

public class UpdateProductRequest
{
    [Required]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? Image { get; set; }
    public int? CategoryId { get; set; }
    public int? Amount { get; set; }
}