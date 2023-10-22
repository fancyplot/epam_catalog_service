﻿using AutoMapper;
using CatalogService.Domain.Models.V1;
using CatalogService.Infrastructure.Models.V1;

namespace CatalogService.Infrastructure.AutoMapper;

public class AutoMapperProfileInfrastructure : Profile
{
    public AutoMapperProfileInfrastructure()
    {
        CreateMap<CategoryEntity, Category>();
        CreateMap<Category, CategoryEntity>();
        CreateMap<Product, ProductEntity>();
        CreateMap<ProductEntity, Product>();
    }
}