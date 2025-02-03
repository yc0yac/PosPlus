using Core.Domain.Entities;

namespace Core.Contracts.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAll();
    Task<bool> Delete(Product product);
    Task<bool> Create(Product product);
    Task<bool> Edit(Product product);
}