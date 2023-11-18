using AutoMapper;
using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Commands.V1.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
{
    private readonly IProductsRepository _productsRepository;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IMapper _mapper;
    private readonly IMessageBroker _messageBroker;
    
    public UpdateProductHandler(IProductsRepository productsRepository, ICategoriesRepository categoriesRepository, IMapper mapper,
        IMessageBroker messageBroker)
    {
        _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
        _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
    }

    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        if (request.CategoryId != null)
        {
            var existingCategory = await _categoriesRepository.GetByIdAsync((int)request.CategoryId, cancellationToken);
            if (existingCategory == null)
                throw new ArgumentException($"Category with id {request.CategoryId} doesn't exist.");
        }

        var existingProduct = await _productsRepository.GetAsync(request.Id, cancellationToken);
        if (existingProduct == null)
            throw new ArgumentException($"Product with id {request.Id} does not exist.");

        if (existingProduct.CategoryId != request.CategoryId && request.CategoryId != null)
        {
            var existingProductWithCategory = await _productsRepository.GetAsync(request.Name, (int)request.CategoryId, cancellationToken);
            if (existingProductWithCategory != null)
                throw new ArgumentException($"Product with name {request.Name} and category id {request.CategoryId} already exists.");
        }

        await _productsRepository.UpdateAsync(product, cancellationToken);

        var updatedProduct = await _productsRepository.GetAsync(request.Id, cancellationToken);

        if (updatedProduct == null)
            throw new Exception($"Product with id {request.Id} was not updated.");

        await _messageBroker.PublishAsync(updatedProduct, cancellationToken);

        return updatedProduct;
    }
}