using Core.Domain.Entities;

namespace Core.Contracts.Services;

public interface ICategoriesService
{
    Task<IEnumerable<Category>> GetAll();
    Task<bool> Delete(Category user);
    Task<bool> Create(Category user);
    Task<bool> Edit(Category user);
}