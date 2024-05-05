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

        UserService = _scope.ServiceProvider.GetService<IUserService>();
        GameService = _scope.ServiceProvider.GetService<IGameService>();
        PlayerService = _scope.ServiceProvider.GetService<IPlayerService>();

    }

    public HttpClient Client { get; init; }
    public IUserService? UserService { get; init; }
    public IGameService? GameService { get; init; }
    public IPlayerService? PlayerService { get; init; }
    public void Dispose()
    {
        Client?.Dispose();
    }
}