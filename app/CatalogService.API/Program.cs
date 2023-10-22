using CatalogService.API.AutoMapper;
using CatalogService.Domain.AutoMapper;
using CatalogService.Domain.Interfaces.V1;
using CatalogService.Domain.Models.V1;
using CatalogService.Infrastructure.AutoMapper;
using CatalogService.Infrastructure.DbContext;
using CatalogService.Infrastructure.Repositories.V1;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Category>());
builder.Services.AddAutoMapper(typeof(AutoMapperProfileInfrastructure), typeof(AutoMapperProfileDomain), typeof(AutoMapperProfileApi));
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer("Server=localhost;Database=EpamLearning;Trusted_Connection=True;TrustServerCertificate=true"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
