using Core.Contracts.Entities;

namespace Core.Contracts.Repositories;

public interface IRepositoryManager
{
    public IUserRepository User { get; }
}