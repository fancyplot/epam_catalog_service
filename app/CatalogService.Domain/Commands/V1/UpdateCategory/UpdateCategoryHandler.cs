using AutoMapper;
using CatalogService.Domain.Commands.V1.CreateCategory;
using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using MediatR;

namespace CatalogService.Domain.Commands.V1.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Category>
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryHandler(ICategoriesRepository cartsRepository, IMapper mapper)
    {
        _categoriesRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request);

        var existingCategory = await _categoriesRepository.GetAsync(request.Id, cancellationToken);
        if (existingCategory == null)
            throw new ArgumentException($"Category with id {request.Id} does not exists");

        await _categoriesRepository.UpdateAsync(category, cancellationToken);

        var createdCategory = await _categoriesRepository.GetAsync(request.Id, cancellationToken);

        if (createdCategory == null)
            throw new Exception($"Category with id {request.Id} does not exists");

        return createdCategory;
    }
}