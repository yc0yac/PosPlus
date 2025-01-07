using Core.Domain.Entities;

namespace Core.Contracts.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<IEnumerable<UserPermission>> GetUserPermissionsAsync(int userId);
    Task<int> UpdateUserPermissionsAsync(int userId, IEnumerable<UserPermission> permissions);
    Task<User?> GetByUsername(string username);
    Task<List<User>?> GetAllAdmins();
}