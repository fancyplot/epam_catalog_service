using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Queries.V1.GetProduct;

public class GetProductHandler : IRequestHandler<GetProductQuery, IEnumerable<Product>>
{
    private readonly IProductsRepository _productsRepository;

    public GetProductHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
    }

    public async Task<IEnumerable<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return await _productsRepository.GetAsync(request.Name, cancellationToken);
    }
}