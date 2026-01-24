using CommunityToolkit.Datasync.Server;
using ExInspectorBackend.Controllers;
using ExInspectorBackend.Middleware;
using ExInspectorBackend.Models;
using ExInspectorBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatasyncServices();
builder.Services.AddControllers();

// Add this with your other service registrations
builder.Services.AddScoped<ApiKeyService>();
builder.Services.AddHostedService<KeyRotationBackgroundService>();

var app = builder.Build();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.InitializeDatabaseAsync().ConfigureAwait(false);
}

// Seed initial API key with a fresh context
using (var seedScope = app.Services.CreateScope())
{
    var seedContext = seedScope.ServiceProvider.GetRequiredService<AppDbContext>();
    await AppDbContext.SeedApiKeyAsync(seedContext).ConfigureAwait(false);
}

app.UseAuthorization();
app.UseMiddleware<Middleware>();

app.UseHttpsRedirection();

// Configure and run the web service.
app.MapControllers();
app.MapBackendEndpoints();
app.Run();