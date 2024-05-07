using CoachConnect.BusinessLayer.DTOs.Login;
using CoachConnect.BusinessLayer.DTOs.Players;
using CoachConnect.DataAccess.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace CoachConnect.IntegrationTests.Controllers;
public class PlayerControllerTests : BaseIntegrationTests
{
    public PlayerControllerTests(CoachConnectWebAppFactory factory)
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

        var query = "?FirstName=Lars";


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

        var playerId = new PlayerId(new Guid("65445678-1234-1234-1234-1234567ccbba"));


        Player player = new()
        {
            Id = playerId,
            FirstName = "Martin",
            LastName = "Ødegård",
            TotalGames = 3,
            TotalPractices = 13,
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
        Assert.Equal(player.FirstName, playerResp.FirstName);
        Assert.Equal(player.LastName, playerResp.LastName);

    }

    
    [Fact]
    public async Task RegisterPlayerAsync_WithValidPlayerData_ReturnsStatusOKAndRegisteredPlayer()
    {
        // arrange
     
        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");
        
        var playerReq = new PlayerRequest
        (
          "Per",
          "Pedersen",
          Guid.Parse("20065784-cdb9-465a-a439-6a627c448ca8"),
          Guid.Parse("ee57d1c3-b41b-4be8-b45e-14f2a25b1001") 
        );

        // act
        
        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();
        
        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        
        var response = await Client.PostAsync("api/v1/players/register", new StringContent(JsonConvert.SerializeObject(playerReq), Encoding.UTF8, "application/json"));
        var registeredPlayer = JsonConvert.DeserializeObject<PlayerResponse>(await response.Content.ReadAsStringAsync());

        // assert            

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(registeredPlayer);
        Assert.Equal(playerReq.FirstName, registeredPlayer.FirstName);
        Assert.Equal(playerReq.LastName, registeredPlayer.LastName);
    }

}


