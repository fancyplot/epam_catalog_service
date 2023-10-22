using AutoMapper;
using CatalogService.Domain.Commands.V1.CreateCategory;
using CatalogService.Domain.Commands.V1.CreateProduct;
using CatalogService.Domain.Commands.V1.UpdateCategory;
using CatalogService.Domain.Commands.V1.UpdateProduct;
using CatalogService.Domain.Models.V1;

namespace CatalogService.Domain.AutoMapper;

public class AutoMapperProfileDomain : Profile
{
    public AutoMapperProfileDomain()
    {
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
    }
}