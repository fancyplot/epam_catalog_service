using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Queries.V1.GetAllCategories;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    private readonly ICategoriesRepository _cartsRepository;

    public GetAllCategoriesHandler(ICategoriesRepository cartsRepository)
    {
        _cartsRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
    }

    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _cartsRepository.GetAllAsync(cancellationToken);
    }
}