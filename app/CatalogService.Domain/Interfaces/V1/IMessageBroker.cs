using CatalogService.Domain.Models.V1;

namespace CatalogService.Domain.Interfaces.V1;

public interface IMessageBroker
{
    Task PublishAsync(Product product, CancellationToken cancellationToken = default);
}