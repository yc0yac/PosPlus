using Core.Contracts.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductsRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : GenericRepository<Product>(contextFactory), IProductsRepository
{
    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        var context = await contextFactory.CreateDbContextAsync();
        return await context.Set<Product>().Include(p=>p.IdCategoryNavigation).ToListAsync();
    }
}