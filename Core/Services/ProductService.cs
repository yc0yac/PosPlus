using Core.Contracts.Providers;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Entities;


namespace Core.Services;

public class ProductService(IRepositoryManager repositoryManager) : IProductService
{
    public async Task<IEnumerable<Product>> GetAll()
    {
        var products = await repositoryManager.Product.GetAllAsync();
        return products.ToList();
    }

    public async Task<bool> Delete(Product product)
    {
        await repositoryManager.Product.DeleteAsync(product);
        return true;
    }

    public async Task<bool> Create(Product product)
    {
        var exists = await repositoryManager.Product.ExistsAsync(c => c.Name.ToLower() == product.Name.ToLower());

        if (exists)
        {
            return false;
        }

        await repositoryManager.Product.AddAsync(product);
        return true;
    }

    public async Task<bool> Edit(Product product)
    {
        await repositoryManager.Product.UpdateAsync(product);
        return true;
    }
}