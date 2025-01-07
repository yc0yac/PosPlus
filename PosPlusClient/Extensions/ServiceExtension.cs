using Core.Contracts.Providers;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Services;
using Infrastructure.Persistence.Configuration;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using PosPlusClient.Components.Pages;
using PosPlusClient.Services;
using Serilog;

namespace PosPlusClient.Extensions;

public static class ServiceExtension
{
    public static void ConfigureContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<ApplicationDbContext>();
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IPasswordServiceProvider, PasswordServiceProvider>();
    }

    public static void ConfigureSerilog(this IHostBuilder host)
    {
        host.UseSerilog((ctx, lc) =>
        {
            lc.WriteTo.Console();
            lc.WriteTo.SQLite(ctx.Configuration.GetConnectionString("default_log"));
        });
    }
}