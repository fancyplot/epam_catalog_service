//using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore;

//namespace CatalogService.Infrastructure.Models.V1;

//public class ProductEntity
//{
//    public Guid Id { get; set; }

//    [Required]
//    [MaxLength(50)]
//    public string Name { get; set; }
    
//    public string Description { get; set; }
    
//    [Required]
//    [Precision(14, 2)]
//    public decimal Price { get; set; }
    
//    public string Image { get; set; }
    
//    [Required]
//    public CategoryEntity Category { get; set; }
    
//    [Required]
//    public int Amount { get; set; }
//}