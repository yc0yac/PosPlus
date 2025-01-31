using Core.Domain.Entities;

namespace Core.Contracts.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<IEnumerable<UsersPermission>> GetUserGeneralPermissionsAsync(int userId);
    Task UpdateUserPermissionsAsync(int userId, IEnumerable<UsersPermission> permissions);
    Task<User?> GetByUsernameAsync(string username);
    Task<List<User>?> GetAllAdminsAsync();
}