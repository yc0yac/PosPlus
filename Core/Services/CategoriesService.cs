using Core.Contracts.Providers;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Entities;


namespace Core.Services;

public class CategoriesService(IRepositoryManager repositoryManager) : ICategoriesService
{
    public async Task<IEnumerable<Category>> GetAll()
    {
        var categories = await repositoryManager.Category.GetAllAsync();
        return categories.ToList();
    }

    public async Task<bool> Delete(Category category)
    {
        await repositoryManager.Category.DeleteAsync(category);
        return true;
    }

    public async Task<bool> Create(Category category)
    {
        var exists = await repositoryManager.Category.ExistsAsync(c => c.Name.ToLower() == category.Name.ToLower());

        if (exists)
        {
            return false;
        }

        await repositoryManager.Category.AddAsync(category);
        return true;
    }

    public async Task<bool> Edit(Category category)
    {
        var exists = await repositoryManager.Category.ExistsAsync(c => c.Name.ToLower() == category.Name.ToLower());

        if (exists)
        {
            return false;
        }

        await repositoryManager.Category.UpdateAsync(category);
        return true;
    }
}