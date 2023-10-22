using AutoMapper;
using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Commands.V1.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductsRepository _productsRepository;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IMapper _mapper;

    public CreateProductHandler(IProductsRepository productsRepository, ICategoriesRepository categoriesRepository, IMapper mapper)
    {
        _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
        _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        var existingCategory = await _categoriesRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (existingCategory == null)
            throw new ArgumentException($"Category with id {request.CategoryId} doesn't exists");

        var existingProduct = await _productsRepository.GetAsync(request.Name, request.CategoryId, cancellationToken);
        if (existingProduct != null)
            throw new ArgumentException($"Product with name {request.Name} and category id {request.CategoryId} already exists.");

        await _productsRepository.CreateAsync(product, cancellationToken);

        var createdProduct = await _productsRepository.GetAsync(request.Name, request.CategoryId, cancellationToken);

        if (createdProduct == null)
            throw new Exception($"Product with name {request.Name} and category id {request.CategoryId} was not created");

        return createdProduct;
    }
}