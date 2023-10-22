using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Queries.V1.GetCategory;

public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, Category>
{
    private readonly ICategoriesRepository _cartsRepository;

    public GetCategoryHandler(ICategoriesRepository cartsRepository)
    {
        _cartsRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
    }

    public async Task<Category> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _cartsRepository.GetByNameAsync(request.Name, cancellationToken);
    }
}