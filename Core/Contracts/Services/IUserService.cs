using Core.Domain.Entities;

namespace Core.Contracts.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    Task<bool> Delete(User user);
    Task<bool> Create(User user);
    Task<bool> Edit(User user);
    Task<IEnumerable<UsersPermission>> GetUserPermissionsAsync(User user);
    Task<bool> UpdateUserPermissionsAsync(User user, IEnumerable<UsersPermission> permissions);
    Task<User?> ValidateCredentials(string username, string password);
    Task<User?> ValidateSharedPassword(string password);
}