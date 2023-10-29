using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Queries.V1.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    private readonly IProductsRepository _productsRepository;

    public GetAllProductsHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
    }

    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productsRepository.GetAllAsync(request.CategoryId, request.PageNumber, request.PageSize, cancellationToken);
    }
}