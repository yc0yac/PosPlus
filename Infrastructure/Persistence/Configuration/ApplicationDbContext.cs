using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Configuration;

public class ApplicationDbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection(string connectionString = "Default")
    {
        string? connection = _configuration.GetConnectionString(connectionString);
        return new SQLiteConnection(connection);

    }
}