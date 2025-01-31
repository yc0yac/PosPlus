using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FluentUI.AspNetCore.Components;
using PosPlusClient.Components;
using PosPlusClient.Extensions;
using PosPlusClient.Services;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    //Logging
    builder.Host.ConfigureSerilog();

    // Add services to the container.
    builder.Services.AddRazorComponents().AddInteractiveServerComponents();
    builder.Services.AddFluentUIComponents();
    
    builder.Services.ConfigureContext(builder.Configuration);
    builder.Services.ConfigureServices(builder.Configuration);
    
    //Auth
    builder.Services.AddScoped<SimpleAuthenticationStateProvider>();
    builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthenticationStateProvider>(provider =>
        provider.GetRequiredService<SimpleAuthenticationStateProvider>());
    
    //AppState
    builder.Services.AddScoped<AppStateProvider>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    /*app.UseAuthentication();
    app.UseAuthorization();*/

    app.UseHttpsRedirection();

    app.UseAntiforgery();
    app.MapStaticAssets();
    app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception On startup.");
}
finally
{
    Log.CloseAndFlush();
}