using Core.Contracts.Entities;
using Core.Contracts.Repositories;

namespace Core.Contracts.Services;

public interface IServiceManager
{
    public IUserService User { get; }
    public ICategoriesService Category { get; }
    public IProductService Product { get; }
}