using CatalogService.Domain.Models.V1;

namespace CatalogService.Domain.Interfaces.V1;

public interface ICategoriesRepository
{
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);

    Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken);

    Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task CreateAsync(Category cart, CancellationToken cancellationToken);

    Task DeleteAsync(string name, CancellationToken cancellationToken);

    Task UpdateAsync(Category category, CancellationToken cancellationToken);
}