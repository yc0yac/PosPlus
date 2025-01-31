using Core.Contracts.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CategoriesRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : GenericRepository<Category>(contextFactory), ICategoriesRepository
{
    
}