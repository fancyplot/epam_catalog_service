using CatalogService.Domain.Interfaces.V1;
using MediatR;

namespace CatalogService.Domain.Commands.V1.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoriesRepository _cartsRepository;

    public DeleteCategoryHandler(ICategoriesRepository cartsRepository)
    {
        _cartsRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _cartsRepository.GetByNameAsync(request.Name, cancellationToken);
        if (existingCategory == null)
            throw new KeyNotFoundException($"Category with name {request.Name} does not exist");

        await _cartsRepository.DeleteAsync(request.Name, cancellationToken);

        var deletedCategory = await _cartsRepository.GetByNameAsync(request.Name, cancellationToken);
        if(deletedCategory != null)
            throw new Exception($"Category with name {request.Name} was not deleted");
    }
}