using Core.Contracts.Providers;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Entities;


namespace Core.Services;

public class UserService(IRepositoryManager repositoryManager, IPasswordServiceProvider passwordServiceProvider) : IUserService
{
    public async Task<IEnumerable<User>> GetAll()
    {
        var users = await repositoryManager.User.GetAll();
        return users.ToList();
    }

    public async Task<bool> Delete(User user)
    {
        return await repositoryManager.User.Delete(user) == 1;
    }

    public async Task<bool> Create(User user)
    {
        user.Username = user.Username?.ToLower();
        user.Password = passwordServiceProvider.HashPassword(user.Password);
        if (user.IsAdmin && !string.IsNullOrEmpty(user.SharedPassword))
        {
            user.SharedPassword = passwordServiceProvider.HashPassword(user.SharedPassword);
        }
        return await repositoryManager.User.Add(user) == 1;
    }

    public async Task<bool> Edit(User user)
    {
        user.Username = user.Username?.ToLower();
        user.Password = passwordServiceProvider.HashPassword(user.Password);
        if (user.IsAdmin && !string.IsNullOrEmpty(user.SharedPassword))
        {
            user.SharedPassword = passwordServiceProvider.HashPassword(user.SharedPassword);
        }
        
        return await repositoryManager.User.Update(user) == 1;
    }

    public async Task<IEnumerable<UserPermission>> GetUserPermissionsAsync(User user)
    {
        var permissions = (await repositoryManager.User.GetUserPermissionsAsync(user.Id)).ToList();
        //Set missing user id
        foreach (var permission in permissions)
        {
            permission.IdUser = user.Id;
        }

        return permissions;
    }

    public async Task<bool> UpdateUserPermissionsAsync(User user, IEnumerable<UserPermission> permissions)
    {
        var result =
            await repositoryManager.User.UpdateUserPermissionsAsync(user.Id,
                permissions.Where(p => p.Granted || p.RequestElevation));
        return result > 0;
    }

    public async Task<User?> ValidateCredentials(string username, string password)
    {
        var user = await repositoryManager.User.GetByUsername(username);
        if (user != null)
        {
            if (!passwordServiceProvider.VerifyPassword(password,user.Password))
            {
                return null;
            }

            user.Password = null;
        }
        user.Permissions = (await GetUserPermissionsAsync(user)).ToList();
        return user;
    }
    
    public async Task<User?> ValidateSharedPassword(string password)
    {
        var users = await repositoryManager.User.GetAllAdmins();

        if (users != null)
        {
            foreach (var user in users)
            {
                
                if (!string.IsNullOrEmpty(user.SharedPassword) && passwordServiceProvider.VerifyPassword(password, user.SharedPassword))
                {
                    return user;
                }
            }
        }
        
        return null;
    }
}