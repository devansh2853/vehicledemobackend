using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;
using VehicleProject.Data;

namespace VehicleProject.IntegrationTests;

public class VehicleProjectWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPortBinding(1433, true)
        .WithEnvironment("name", "devansh")
        .WithEnvironment("hostname", "localdevansh")
        .WithPassword("Strong_password_123!")
        .WithEnvironment("ACCEPT_EULA", "Y")
        .Build();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<VehicleProjectContext>>();
            services.AddDbContext<VehicleProjectContext>(options =>
            {
                options.UseSqlServer(_msSqlContainer.GetConnectionString());
            });
            // services.AddSqlServer<VehicleProjectContext>(connString);
            // using (var scope = services.BuildServiceProvider().CreateScope())
            // {
            //     var dbContext = scope.ServiceProvider.GetRequiredService<VehicleProjectContext>();
            //     dbContext.Database.Migrate();
            // }
            // var dbcontext =  CreateDbContext(services);
            // dbcontext.Database.Migrate();
        });
    }

    // private static string? GetConnectionString()
    // {
    //     var configuration = new ConfigurationBuilder()
    //         .AddUserSecrets<VehicleProjectWebApplicationFactory>()
    //         .Build();
    //     
    //     var connString = configuration.GetConnectionString("DefaultConnection");
    //     return connString;
    // }

    // private static VehicleProjectContext CreateDbContext(IServiceCollection services)
    // {
    //     var serviceProvider = services.BuildServiceProvider();
    //     var scope = serviceProvider.CreateScope();
    //     var dbContext = scope.ServiceProvider.GetRequiredService<VehicleProjectContext>();
    //     return dbContext;
    // }

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _msSqlContainer.StopAsync();
    }
}