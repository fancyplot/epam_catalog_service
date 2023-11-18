using AutoMapper;
using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using MassTransit;
using OnlineStore.Contracts;

namespace CatalogService.Infrastructure.MessageBroker;

public class MessageBroker : IMessageBroker
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;

    public MessageBroker(IMapper mapper, IBus bus)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }
    
    public async Task PublishAsync(Product product, CancellationToken cancellationToken = default)
    {
        var message = _mapper.Map<ProductMessage>(product);

        await _bus.Publish(message, cancellationToken);
    }
}