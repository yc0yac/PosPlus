using System.Data;
using System.Transactions;
using Core.Contracts.Repositories;
using Core.Domain.Entities;
using Dapper;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UsersRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : GenericRepository<User>(contextFactory), IUserRepository
{
    public async Task<User?> GetByUsernameAsync(string username)
    {
        var context = await contextFactory.CreateDbContextAsync();
        return await context.Users.Include(u=>u.UsersPermissions).FirstOrDefaultAsync(u => u.Username.ToUpper().Equals(username.ToUpper()));
    }

    public async Task<List<User>?> GetAllAdminsAsync()
    {
        var context = await contextFactory.CreateDbContextAsync();
        return await context.Users.Where(u => u.Isadmin).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<UsersPermission>> GetUserGeneralPermissionsAsync(int userId)
    {
        var context = await contextFactory.CreateDbContextAsync();
        var allPermissions = await context.Permissions.AsNoTracking().ToListAsync();
        var userPermissions = await context.UsersPermissions.Where(up => up.IdUser==userId).Include(up=>up.IdPermissionNavigation).AsNoTracking().ToListAsync();

        foreach (var ap in allPermissions.Where(ap => !userPermissions.Exists(up => up.IdPermission == ap.Id)))
        {
            userPermissions.Add(new UsersPermission()
            {
                Granted = false,
                IdPermission = ap.Id,
                IdUser = userId,
                RequestElevation = false,
                IdPermissionNavigation = new Permission()
                {
                    Name = ap.Name,
                    Area = ap.Area,
                    Description = ap.Description
                }
            });
        }

        return userPermissions;
    }

    public async Task UpdateUserPermissionsAsync(int userId, IEnumerable<UsersPermission> permissions)
    {
        var context = await contextFactory.CreateDbContextAsync();
        
        // Obtener el usuario
        var user = await context.Users
            .Include(u => u.UsersPermissions) // Incluir los permisos actuales
            .FirstOrDefaultAsync(u => u.Id == userId);
        
        // Filtrar solo los permisos válidos
        var validPermissions = permissions.Where(up => up.Granted || up.RequestElevation).ToList();

        // Eliminar los permisos antiguos del usuario
        context.UsersPermissions.RemoveRange(user.UsersPermissions);
       
        // Eliminar Navegacion a permisos
        foreach (var permission in validPermissions)
        {
            permission.IdPermissionNavigation = null;
        }

        // Agregar los nuevos permisos
        user.UsersPermissions = validPermissions;
        
        await context.SaveChangesAsync();
    }
}