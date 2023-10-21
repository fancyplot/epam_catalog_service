using AutoMapper;
using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using CatalogService.Infrastructure.DbContext;
using CatalogService.Infrastructure.Models.V1;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories.V1;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;


    public CategoriesRepository(IMapper mapper, ApplicationContext context)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = _context.Categories;

        return Task.FromResult(_mapper.Map<IEnumerable<Category>>(categories));
    }

    public Task<Category> GetAsync(string name, CancellationToken cancellationToken)
    {
        var entity = _context.Categories.FirstOrDefault(t => t.Name == name);

        return Task.FromResult(_mapper.Map<Category>(entity));
    }

    public Task<Category> GetAsync(int id, CancellationToken cancellationToken)
    {
        var entity = _context.Categories.FirstOrDefault(t => t.Id == id);

        return Task.FromResult(_mapper.Map<Category>(entity));
    }

    public async Task CreateAsync(Category category, CancellationToken cancellationToken)
    {
        var categoryEntity = _mapper.Map<CategoryEntity>(category);
        _context.Categories.Add(categoryEntity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(string name, CancellationToken cancellationToken)
    {
        var entity = _context.Categories.Include(p => p.Child).FirstOrDefault(t => t.Name == name);
        _context.Remove(entity);
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        var categoryEntity = _mapper.Map<CategoryEntity>(category);

        var entity = _context.Categories.FirstOrDefault(t => t.Id == categoryEntity.Id);
        if (categoryEntity.Image != null)
            entity.Image = categoryEntity.Image;

        if(categoryEntity.Name != null)
            entity.Name = categoryEntity.Name;

        entity.ParentId = categoryEntity.ParentId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}