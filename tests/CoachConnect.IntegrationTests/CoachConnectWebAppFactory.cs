using CoachConnect.DataAccess.Data;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;

namespace CoachConnect.IntegrationTests;

public class CoachConnectWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MySqlContainer _mySqlContainer;
    private readonly int _port = Random.Shared.Next(5000, 9000);

    public CoachConnectWebAppFactory()
    {
        _mySqlContainer = new MySqlBuilder()
            .WithImage("kiwiketil/coachconnect_db_img:v1")
            .WithDatabase("coach_connect")
            .WithUsername("{COACH_CONNECT_USERNAME}")
            .WithPassword("{COACH_CONNECT_PASSWORD}")
            .WithPortBinding(_port)
            .Build();            
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.FirstOrDefault(s => s.ServiceType == typeof(DbContextOptions<CoachConnectDbContext>));
            if (descriptor != null) 
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<CoachConnectDbContext>(options => 
            {
                options.UseMySql(
                    _mySqlContainer.GetConnectionString(),
                    new MySqlServerVersion(ServerVersion
                    .AutoDetect(_mySqlContainer.GetConnectionString())),
                    builder => 
                    {
                        builder.EnableRetryOnFailure();
                    });
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _mySqlContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _mySqlContainer.StopAsync();
    }
}