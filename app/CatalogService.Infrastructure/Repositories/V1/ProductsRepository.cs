using AutoMapper;
using AutoMapper.Configuration.Conventions;
using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using CatalogService.Infrastructure.DbContext;
using CatalogService.Infrastructure.Models.V1;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories.V1;

public class ProductsRepository : IProductsRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;


    public ProductsRepository(IMapper mapper, ApplicationContext context)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = _context.Products;

        return Task.FromResult(_mapper.Map<IEnumerable<Product>>(products));
    }

    public Task<IEnumerable<Product>> GetAsync(string name, CancellationToken cancellationToken)
    {
        var entity = _context.Products.Where(t => t.Name == name);

        return Task.FromResult(_mapper.Map<IEnumerable<Product>>(entity));
    }

    public Task<Product> GetAsync(string name, int categoryId, CancellationToken cancellationToken)
    {
        var entity = _context.Products.FirstOrDefault(t => t.Name == name && t.CategoryId == categoryId);

        return Task.FromResult(_mapper.Map<Product>(entity));
    }

    public Task<Product> GetAsync(int id, CancellationToken cancellationToken)
    {
        var entity = _context.Products.FirstOrDefault(t => t.Id == id);

        return Task.FromResult(_mapper.Map<Product>(entity));
    }

    public async Task CreateAsync(Product product, CancellationToken cancellationToken)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        _context.Products.Add(productEntity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(string name, int categoryId, CancellationToken cancellationToken)
    {
        var entity = _context.Products.FirstOrDefault(t => t.Name == name && t.CategoryId == categoryId);
        _context.Remove(entity);
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {

        var entity = _context.Products.FirstOrDefault(t => t.Id == product.Id);
        if (product.Image != null)
            entity.Image = product.Image;

        if(product.Description != null)
            entity.Description = product.Description;

        if(product.Name != null)
            entity.Name = product.Name;

        if (product.Amount != null)
            entity.Amount = (int)product.Amount;

        if (product.CategoryId != null)
            entity.CategoryId = (int)product.CategoryId;

        if (product.Price != null)
            entity.Price = (int)product.Price;

        await _context.SaveChangesAsync(cancellationToken);
    }
}