using CatalogService.Domain.Models.V1;

namespace CatalogService.Domain.Interfaces.V1;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetAllAsync(int? categoryId, int? pageNumber, int? pageSize, CancellationToken cancellationToken);

    Task<IEnumerable<Product>> GetAsync(string name, CancellationToken cancellationToken);

    Task<Product> GetAsync(int id, CancellationToken cancellationToken);

    Task<Product> GetAsync(string name, int categoryId, CancellationToken cancellationToken);

    Task CreateAsync(Product cart, CancellationToken cancellationToken);

    Task DeleteAsync(string name, int categoryId, CancellationToken cancellationToken);

    Task UpdateAsync(Product category, CancellationToken cancellationToken);
}