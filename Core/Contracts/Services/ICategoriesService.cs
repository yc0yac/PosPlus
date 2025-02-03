using Core.Domain.Entities;

namespace Core.Contracts.Services;

public interface ICategoriesService
{
    Task<IEnumerable<Category>> GetAll();
    Task<bool> Delete(Category category);
    Task<bool> Create(Category category);
    Task<bool> Edit(Category category);
}