using AutoMapper;
using CatalogService.Domain.Models.V1;
using CatalogService.Infrastructure.Models.V1;

namespace CatalogService.Infrastructure.AutoMapper;

public class AutoMapperProfileInfrastructure : Profile
{
    public AutoMapperProfileInfrastructure()
    {
        CreateMap<CategoryEntity, Category>();
           // .ForMember(t => t.Id, opt => opt.MapFrom(src => src.CartId));
           CreateMap<Category, CategoryEntity>();
           // .ForMember(t => t.CartId, opt => opt.MapFrom(src => src.Id));
    }
}