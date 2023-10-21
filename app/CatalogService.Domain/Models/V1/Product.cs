namespace CatalogService.Domain.Models.V1;

public class Product
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public Category Category { get; set; }
    public int Amount { get; set; }
    
}