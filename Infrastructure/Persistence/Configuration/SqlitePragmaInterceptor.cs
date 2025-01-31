using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace Infrastructure.Persistence.Configuration;

public class SqlitePragmaInterceptor : IDbConnectionInterceptor
{
    public void ConnectionOpened(DbConnection connection, ConnectionEndEventData eventData)
    {
        if (connection is Microsoft.Data.Sqlite.SqliteConnection sqliteConnection)
        {
            using (var command = sqliteConnection.CreateCommand())
            {
                command.CommandText = "PRAGMA journal_mode=WAL;";
                command.ExecuteNonQuery();
            }
        }
    }
    // Implementa otros métodos de IDbConnectionInterceptor como vacíos si no los necesitas
    public void ConnectionOpening(DbConnection connection, ConnectionEventData eventData) { }
    public void ConnectionFailed(DbConnection connection, ConnectionErrorEventData eventData) { }
    public void ConnectionClosed(DbConnection connection, ConnectionEndEventData eventData) { }
    public void ConnectionClosing(DbConnection connection, ConnectionEventData eventData) { }
}