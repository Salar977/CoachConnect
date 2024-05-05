using CoachConnect.BusinessLayer.Services;
using CoachConnect.BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CoachConnect.IntegrationTests;

public class BaseIntegrationTests : IClassFixture<CoachConnectWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;   

    public BaseIntegrationTests(CoachConnectWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Client = factory.CreateClient();
    }

    public HttpClient Client { get; init; }

    public void Dispose()
    {
        Client?.Dispose();
    }
}