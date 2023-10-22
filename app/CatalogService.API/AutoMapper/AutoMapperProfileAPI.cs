using AutoMapper;
using CatalogService.API.Models.V1;
using CatalogService.Domain.Commands.V1.UpdateProduct;

namespace CatalogService.API.AutoMapper;

public class AutoMapperProfileApi : Profile
{
    public AutoMapperProfileApi()
    {
        CreateMap<UpdateProductRequest, UpdateProductCommand>();
    }
}