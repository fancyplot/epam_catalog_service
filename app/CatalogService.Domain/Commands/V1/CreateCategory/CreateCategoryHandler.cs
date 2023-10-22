using AutoMapper;
using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Commands.V1.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(ICategoriesRepository cartsRepository, IMapper mapper)
    {
        _categoriesRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request);

        var existingCategory = await _categoriesRepository.GetByNameAsync(request.Name, cancellationToken);
        if (existingCategory != null)
            throw new ArgumentException($"Category with name {request.Name} already exists");

        await _categoriesRepository.CreateAsync(category, cancellationToken);

        var createdCategory = await _categoriesRepository.GetByNameAsync(request.Name, cancellationToken);

        if (createdCategory == null)
            throw new Exception($"Category with name {request.Name} was not created");

        return createdCategory;
    }
}