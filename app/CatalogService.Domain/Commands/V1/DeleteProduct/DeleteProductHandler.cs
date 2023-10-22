using CatalogService.Domain.Interfaces.V1;
using MediatR;

namespace CatalogService.Domain.Commands.V1.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductsRepository _productsRepository;

    public DeleteProductHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _productsRepository.GetAsync(request.Name, request.CategoryId, cancellationToken);
        if (existingProduct == null)
            throw new KeyNotFoundException($"Product with name {request.Name} and category id {request.CategoryId} does not exist");

        await _productsRepository.DeleteAsync(request.Name, request.CategoryId, cancellationToken);

        var deletedProduct = await _productsRepository.GetAsync(request.Name, request.CategoryId, cancellationToken);
        if (deletedProduct != null)
            throw new Exception($"Product with name {request.Name} and category id {request.CategoryId} was not deleted");
    }
}