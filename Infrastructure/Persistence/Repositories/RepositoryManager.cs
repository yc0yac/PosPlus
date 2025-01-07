using System.Data.SQLite;
using Core.Contracts.Entities;
using Core.Contracts.Repositories;
using Dapper;
using Infrastructure.Persistence.Configuration;

namespace Infrastructure.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    //Implement Lazy Rep
    private readonly Lazy<IUserRepository> _userRepository;

    public RepositoryManager(ApplicationDbContext context)
    {
        Configure(context);
        
        _userRepository = new Lazy<IUserRepository>(() => new UsersRepository(context));
    }

    //Get Implemented Rep
    public IUserRepository User => _userRepository.Value;
    
    
    
    
    //Configure Repos
    private void Configure(ApplicationDbContext context)
    {
        //Configure wal
        using (var connection = context.CreateConnection())
        {
            connection.Execute("PRAGMA journal_mode=WAL;");
        }
    }
}