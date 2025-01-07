using System.Data;
using System.Transactions;
using Core.Contracts.Repositories;
using Core.Domain.Entities;
using Dapper;
using Infrastructure.Persistence.Configuration;

namespace Infrastructure.Persistence.Repositories;

public class UsersRepository : GenericRepository<User>, IUserRepository
{
    private readonly IDbConnection _connection;

    public UsersRepository(ApplicationDbContext context) : base(context)
    {
        _connection = context.CreateConnection();
    }

    public async Task<User?> GetByUsername(string username)
    {
        User? result;
        try
        {
            const string query = "SELECT * FROM Users WHERE lower(Users.username) = lower(@username)";

            result = await _connection.QuerySingleOrDefaultAsync<User>(query, new { username });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching a record from db: ${ex.Message}");
            throw new Exception("Unable to fetch data. Please contact the administrator.");
        }
        finally
        {
            _connection.Close();
        }

        return result;
    }

    public async Task<List<User>?> GetAllAdmins()
    {
        List<User> result;
        try
        {
            const string query = "SELECT shared_password as sharedpassword,isadmin,position,salary,email,disabled,photo,username,password,name,id FROM Users WHERE Users.isadmin = 1 and Users.disabled = 0";

            result = (await _connection.QueryAsync<User>(query)).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching a record from db: ${ex.Message}");
            throw new Exception("Unable to fetch data. Please contact the administrator.");
        }
        finally
        {
            _connection.Close();
        }

        return result;
    }

    public async Task<IEnumerable<UserPermission>> GetUserPermissionsAsync(int userId)
    {
        IEnumerable<UserPermission> result;
        try
        {
            const string query = "SELECT P.id, P.name, P.description, P.area, " +
                                 "PU.id_user, PU.granted, PU.request_elevation AS RequestElevation " +
                                 "FROM Permissions AS P " +
                                 "LEFT JOIN (SELECT * FROM Users_Permissions " +
                                 "WHERE Users_Permissions.id_user = @UserId) AS PU " +
                                 "ON P.id = PU.id_permission";

            result = await _connection.QueryAsync<UserPermission>(query, new { UserId = userId });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching records from db: ${ex.Message}");
            throw new Exception("Unable to fetch data. Please contact the administrator.");
        }
        finally
        {
            _connection.Close();
        }

        return result;
    }

    public async Task<int> UpdateUserPermissionsAsync(int userId, IEnumerable<UserPermission> permissions)
    {
        const string deleteSql = "DELETE FROM Users_Permissions WHERE id_user = @UserId";
        const string insertSql = "INSERT INTO Users_Permissions (id_user, id_permission, granted, request_elevation) VALUES  (@IdUser, @PermissionId, @Granted, @RequestElevation)";

        var trimmedPermissions = new List<object>();
        foreach (var permission in permissions)
        {
            trimmedPermissions.Add(new
            {
                IdUser = permission.IdUser, PermissionId = permission.Id, Granted = permission.Granted,
                RequestElevation = permission.RequestElevation
            });
        }

        var rowsAffected = -1;

        try
        {
            _connection.Open();
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    //First delete al user permissions
                    rowsAffected += await _connection.ExecuteAsync(deleteSql, new { UserId = userId });
                    //Insert the permissions
                    rowsAffected += await _connection.ExecuteAsync(insertSql, trimmedPermissions);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        catch (Exception ex)
        {
            return -1;
        }
        finally
        {
            _connection.Close();
        }

        return rowsAffected;
    }
}