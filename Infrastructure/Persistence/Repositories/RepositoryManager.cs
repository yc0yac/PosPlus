using System.Data.SQLite;
using Core.Contracts.Entities;
using Core.Contracts.Repositories;
using Core.Domain.Entities;
using Dapper;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.Persistence.Repositories;

public class RepositoryManager(IDbContextFactory<ApplicationDbContext> contextFactory) : IRepositoryManager
{
    private readonly Lazy<IUserRepository> _lazyUserRepository = new(()=>new UsersRepository(contextFactory));
    private readonly Lazy<ICategoriesRepository> _lazyCategoriesRepository = new(()=>new CategoriesRepository(contextFactory));

    
    
    public IUserRepository User => _lazyUserRepository.Value; 
    public ICategoriesRepository Category => _lazyCategoriesRepository.Value; 

    

    
    
    
    
    
    
    
    
    
    
  
    

    
    
    
}