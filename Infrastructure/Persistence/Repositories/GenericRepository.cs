using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Core.Contracts.Repositories;
using Dapper;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity>(IDbContextFactory<ApplicationDbContext> contextFactory) 
             : IGenericRepository<TEntity>  where TEntity : class
{
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var context = await contextFactory.CreateDbContextAsync();
        return await context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var context = await contextFactory.CreateDbContextAsync();
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        var context = await contextFactory.CreateDbContextAsync();
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var context = await contextFactory.CreateDbContextAsync();
        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        var context = await contextFactory.CreateDbContextAsync();
        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var context = await contextFactory.CreateDbContextAsync();
        return await context.Set<TEntity>().AnyAsync(predicate);
    }
}