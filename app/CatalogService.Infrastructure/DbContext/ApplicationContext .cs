using CatalogService.Infrastructure.Models.V1;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.DbContext;

public class ApplicationContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<CategoryEntity> Categories { get; set; }
    //public DbSet<ProductEntity> Products { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CategoryEntity>()
            .Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder
             .Entity<CategoryEntity>()
             .HasOne(e => e.Parent)
             .WithMany(e => e.Child)
             .HasForeignKey(e => e.ParentId)
             .OnDelete(DeleteBehavior.ClientSetNull);
    }
}