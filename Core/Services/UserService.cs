using Core.Contracts.Providers;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Entities;


namespace Core.Services;

public class UserService(IRepositoryManager repositoryManager, IPasswordServiceProvider passwordServiceProvider) : IUserService
{
    public async Task<IEnumerable<User>> GetAll()
    {
        var users = await repositoryManager.User.GetAllAsync();
        return users.ToList();
    }

    public async Task<bool> Delete(User user)
    {
        await repositoryManager.User.DeleteAsync(user);
        return true;
    }

    public async Task<bool> Create(User user)
    {
        user.Username = user.Username?.ToLower();
        user.Password = passwordServiceProvider.HashPassword(user.Password);
        if (user.Isadmin && !string.IsNullOrEmpty(user.SharedPassword))
        {
            user.SharedPassword = passwordServiceProvider.HashPassword(user.SharedPassword);
        }
        await repositoryManager.User.AddAsync(user);
        return true;
    }

    public async Task<bool> Edit(User user)
    {
        user.Username = user.Username.ToLower();
        user.Password = passwordServiceProvider.HashPassword(user.Password);
        if (user.Isadmin && !string.IsNullOrEmpty(user.SharedPassword))
        {
            user.SharedPassword = passwordServiceProvider.HashPassword(user.SharedPassword);
        }
        
        await repositoryManager.User.UpdateAsync(user);
        return true;
    }

    public async Task<IEnumerable<UsersPermission>> GetUserPermissionsAsync(User user)
    {
        var permissions = (await repositoryManager.User.GetUserGeneralPermissionsAsync(user.Id)).ToList();
        return permissions;
    }

    public async Task<bool> UpdateUserPermissionsAsync(User user, IEnumerable<UsersPermission> permissions)
    {
            await repositoryManager.User.UpdateUserPermissionsAsync(user.Id,
                permissions.Where(p => p.Granted || p.RequestElevation));
        return true;
    }

    public async Task<User?> ValidateCredentials(string username, string password)
    {
        var user = await repositoryManager.User.GetByUsernameAsync(username);
        if (user != null)
        {
            if (!passwordServiceProvider.VerifyPassword(password,user.Password))
            {
                return null;
            }

            user.Password = null;
        }
        
        return user;
    }
    
    public async Task<User?> ValidateSharedPassword(string password)
    {
        var users = await repositoryManager.User.GetAllAdminsAsync();

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