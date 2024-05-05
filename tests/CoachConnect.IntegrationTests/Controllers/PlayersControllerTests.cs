using CoachConnect.BusinessLayer.DTOs.Login;
using CoachConnect.BusinessLayer.DTOs.Players;
using CoachConnect.BusinessLayer.DTOs.Users;
using CoachConnect.DataAccess.Entities;
using System.Net;
using System.Net.Http.Json;

namespace CoachConnect.IntegrationTests.Controllers;
public class PlayersControllerTests : BaseIntegrationTests
{
    public PlayersControllerTests(CoachConnectWebAppFactory factory)
    : base(factory)
    {

    }
    [Fact]
    public async Task GetPlayersAsync_DefaultPageSizeAndEmptyQuery_ReturnStatusOKAndPlayers()
    {
        // arrange


        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync("api/v1/players");
        var responseData = await response.Content.ReadAsStringAsync();
        var playerResp = System.Text.Json.JsonSerializer.Deserialize<List<PlayerResponse>>(responseData);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(playerResp);
        Assert.Equal(10, playerResp.ToList().Count);
    }

    [Fact]
    public async Task GetPlayerByFirstNameAsync_UsingValidQuery_ReturnsOKAndDefaultSizeListPlayer()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var query = "?FirstName=Frode";

        /*
        Player player = new()
        {
            //Id = Id,
            FirstName = "Kristian",
            LastName = "Walin",
            Created = new DateTime(2024, 04, 17, 13, 00, 49, 312),
        };
        */

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync($"api/v1/players{query}");
        var responseData = await response.Content.ReadAsStringAsync();
        var playerResp = System.Text.Json.JsonSerializer.Deserialize<List<PlayerResponse>>(responseData);
        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(playerResp);
    }

    [Fact]
    public async Task GetPlayerByIdAsync_WithValidId_Returns_StatusOkAndPlayer()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var playerId = new PlayerId(new Guid("92752322-5353-8888-5232-226352223422"));
        var teamId = new TeamId(new Guid("22752322-3333-8888-9999-226352223422"));
        var userId = new UserId(new Guid("33333333-2233-8888-9999-226352223422"));

        Player player = new()
        {
            Id = playerId,
            TeamId = teamId,
            UserId = userId,
            FirstName = "Kristian",
            LastName = "Walin",
            TotalGames = 0,
            TotalPractices = 5,
            Created = new DateTime(2024, 04, 17, 13, 00, 49, 312),
            Updated = new DateTime(2024, 04, 17, 13, 00, 49, 312),
        };

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync($"api/v1/players/{playerId.playerId}");
        var playerResp = await response.Content.ReadFromJsonAsync<PlayerResponse>();

        // assert
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(playerResp);
        //Assert.Equal(player, playerResp);
        Assert.Equal(player.FirstName, playerResp.FirstName);
        Assert.Equal(player.LastName, playerResp.LastName);
    }
}


