using Core.Contracts.Providers;
using Core.Contracts.Repositories;
using Core.Contracts.Services;

namespace Core.Services;

public class ServiceManager(IRepositoryManager repositoryManager, IPasswordServiceProvider passwordServiceProvider) : IServiceManager
{
    //Implement Lazy Svc
    private readonly Lazy<IUserService> _userService = new(() => new UserService(repositoryManager,passwordServiceProvider));
    private readonly Lazy<ICategoriesService> _categoriesService = new(() => new CategoriesService(repositoryManager));
    private readonly Lazy<IProductService> _productService = new(() => new ProductService(repositoryManager));
    
    //Get Implemented Svc
    public IUserService User => _userService.Value;
    public ICategoriesService Category => _categoriesService.Value;
    public IProductService Product => _productService.Value;
}