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

    public async Task<bool> Create(Category Category)
    {
        await repositoryManager.Category.AddAsync(Category);
        return true;
    }

    public async Task<bool> Edit(Category Category)
    {
        await repositoryManager.Category.UpdateAsync(Category);
        return true;
    }

    
}