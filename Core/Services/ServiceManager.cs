﻿using Core.Contracts.Providers;
using Core.Contracts.Repositories;
using Core.Contracts.Services;

namespace Core.Services;

public class ServiceManager(IRepositoryManager repositoryManager, IPasswordServiceProvider passwordServiceProvider) : IServiceManager
{
    //Implement Lazy Svc
    private readonly Lazy<IUserService> _userService = new(() => new UserService(repositoryManager,passwordServiceProvider));
    private readonly Lazy<ICategoriesService> _categoriesService = new(() => new CategoriesService(repositoryManager));
    
    
    //Get Implemented Svc
    public IUserService User => _userService.Value;
    public ICategoriesService Category => _categoriesService.Value;
}