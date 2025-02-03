using Core.Contracts.Entities;

namespace Core.Contracts.Repositories;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    ICategoriesRepository Category { get; }
    IProductsRepository Product { get; }
}