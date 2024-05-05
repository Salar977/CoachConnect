using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CoachConnect.BusinessLayer.DTOs.Users;
using CoachConnect.BusinessLayer.DTOs.Games;
using System.Net.Http.Json;
using CoachConnect.BusinessLayer.DTOs.Login;
using Newtonsoft.Json;

namespace CoachConnect.IntegrationTests.Controllers;
public class GamesControllerTests : BaseIntegrationTests
{
    public GamesControllerTests(CoachConnectWebAppFactory factory)
    : base(factory)
    {
    }

    [Fact]
    public async Task GetGamesAsync_DefaultPageSizeAndEmptyQuery_ReturnsStatusOKAndGames()
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

        var response = await Client!.GetAsync("api/v1/games");
        var responseData = await response.Content.ReadAsStringAsync();
        var gameDtos = System.Text.Json.JsonSerializer.Deserialize<List<GameDTO>>(responseData);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(gameDtos);
        Assert.Equal(10, gameDtos.ToList().Count);
    }

    [Fact]
    public async Task GetGameByIdAsync_WithValidGameId_Returns_ReturnsStatusOKAndGame()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var gameId = new GameId(new Guid("2f042e86-d75e-4591-a810-aca808725555"));

        var homeTeamId = new TeamId(Guid.Parse("a3b2a7e5-b0e2-40e2-a42d-69e10a22d011"));
        var awayTeamId = new TeamId(Guid.Parse("b01b6b08-2f43-4be5-b40b-7b9fd2d3d009"));

        Game game = new ()
        {
            Id = gameId,
            Location = "Bærum",
            HomeTeam = homeTeamId,
            AwayTeam = awayTeamId,
            GameTime = new DateTime(2024, 04, 17, 13, 00, 49, 312), 
        };

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync($"api/v1/games/{gameId.gameId}");
        var gameDto = await response.Content.ReadFromJsonAsync<GameDTO>();

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(gameDto);
        Assert.Equal(game.Id, gameDto.Id);
        Assert.Equal(game.Location, gameDto.Location);
        Assert.Equal(game.HomeTeam, gameDto.HomeTeam);
        Assert.Equal(game.AwayTeam, gameDto.AwayTeam);
        Assert.Equal(game.GameTime, gameDto.GameTime);
    }

    [Fact]
    public async Task UpdateGameAsync_WithValidGameId_WithValidCoachId_ReturnsStatusOKAndUpdatedGame()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var gameId = new GameId(new Guid("2f042e86-d75e-4591-a810-aca80872aaa5"));

        var homeTeamId = new TeamId(Guid.Parse("ee57d1c3-b41b-4be8-b45e-14f2a25b1001"));
        var awayTeamId = new TeamId(Guid.Parse("b01b6b08-2f43-4be5-b40b-7b9fd2d3d009"));

        var gameUpdateDto = new GameUpdateDTO
        (
            "Stockholm",
            homeTeamId,
            awayTeamId,
            new DateTime(2024, 09, 17, 13, 00, 49, 312)
        );            

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PutAsync($"api/v1/games/{gameId.gameId}", new StringContent(JsonConvert.SerializeObject(gameUpdateDto), Encoding.UTF8, "application/json"));
        var responseDTO = await response.Content.ReadFromJsonAsync<GameUpdateDTO>();

        //assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(responseDTO);
        Assert.Equal(gameUpdateDto.Location, responseDTO.Location);
        Assert.Equal(gameUpdateDto.HomeTeam, responseDTO.HomeTeam);
        Assert.Equal(gameUpdateDto.AwayTeam, responseDTO.AwayTeam);
        Assert.Equal(gameUpdateDto.GameTime, responseDTO.GameTime);
    }

    [Fact]
    public async Task UpdateGameAsync_WithValidGameId_WithUnathorizedCoachId_AsCoachIsNotEitherTeamsCoach_ReturnsBadRequest()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var gameId = new GameId(new Guid("2f042e86-d75e-4591-a810-aca80872a930"));

        var homeTeamId = new TeamId(Guid.Parse("a3b2a7e5-b0e2-40e2-a42d-69e10a22d011"));
        var awayTeamId = new TeamId(Guid.Parse("b01b6b08-2f43-4be5-b40b-7b9fd2d3d009"));

        var gameUpdateDto = new GameUpdateDTO
        (
            "Stockholm",
            homeTeamId,
            awayTeamId,
            new DateTime(2024, 09, 17, 13, 00, 49, 312)
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PutAsync($"api/v1/games/{gameId.gameId}", new StringContent(JsonConvert.SerializeObject(gameUpdateDto), Encoding.UTF8, "application/json"));

        //assert

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task CreateGameAsync_WithValidCoachId_ReturnsOKAndRegisteredGame()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var homeTeamId = new TeamId(Guid.Parse("ee57d1c3-b41b-4be8-b45e-14f2a25b1001"));
        var awayTeamId = new TeamId(Guid.Parse("b01b6b08-2f43-4be5-b40b-7b9fd2d3d009"));

        var gameRegistrationDto = new GameRegistrationDTO
        (
            "Frankfurt",
            homeTeamId,
            awayTeamId,
            new DateTime(2024, 10, 24, 00, 22, 22, 333)
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PostAsync($"api/v1/games/register", new StringContent(JsonConvert.SerializeObject(gameRegistrationDto), Encoding.UTF8, "application/json"));
        var responseDTO = await response.Content.ReadFromJsonAsync<GameRegistrationDTO>();

        //assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(responseDTO);
        Assert.Equal(gameRegistrationDto.Location, responseDTO.Location);        
        Assert.Equal(gameRegistrationDto.HomeTeam, responseDTO.HomeTeam);        
        Assert.Equal(gameRegistrationDto.AwayTeam, responseDTO.AwayTeam);        
        Assert.Equal(gameRegistrationDto.GameTime, responseDTO.GameTime);        
    }

    [Fact]
    public async Task CreateGameAsync_WithUnathorizedCoachId_AsCoachIsNotEitherTeamsCoach_ReturnsBadRequest()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "sol@epost.com", Password = "S1lskjær#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var homeTeamId = new TeamId(Guid.Parse("ee57d1c3-b41b-4be8-b45e-14f2a25b1001"));
        var awayTeamId = new TeamId(Guid.Parse("b01b6b08-2f43-4be5-b40b-7b9fd2d3d009"));

        var gameRegistrationDto = new GameRegistrationDTO
        (
            "Frankfurt",
            homeTeamId,
            awayTeamId,
            new DateTime(2024, 12, 24, 00, 22, 22, 333)
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PostAsync($"api/v1/games/register", new StringContent(JsonConvert.SerializeObject(gameRegistrationDto), Encoding.UTF8, "application/json"));

        //assert

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task CreateGameAsync_WithValidCoachId_WhereTeamGameAlreadyExistOnDate_ReturnsBadRequest()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "ottis@epost.no", Password = "O1tesen#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var homeTeamId = new TeamId(Guid.Parse("b2d84d4e-921c-4c17-af43-18d13b105004"));
        var awayTeamId = new TeamId(Guid.Parse("b01b6b08-2f43-4be5-b40b-7b9fd2d3d009"));

        var gameRegistrationDto = new GameRegistrationDTO
        (
            "Frankfurt",
            homeTeamId,
            awayTeamId,
            new DateTime(2024, 12, 06, 09, 30, 49, 312)
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PostAsync($"api/v1/games/register", new StringContent(JsonConvert.SerializeObject(gameRegistrationDto), Encoding.UTF8, "application/json"));

        //assert

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task DeleteGameAsync_WithValidCoachId_ReturnsOKAndDeletedGame()
    {
        //// arrange

        //LoginDTO loginDto = new() { Username = "ottis@epost.no", Password = "O1tesen#" };
        //var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        //StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        //var homeTeamId = new TeamId(Guid.Parse("b2d84d4e-921c-4c17-af43-18d13b105004"));
        //var awayTeamId = new TeamId(Guid.Parse("b01b6b08-2f43-4be5-b40b-7b9fd2d3d009"));

        //var gameRegistrationDto = new GameRegistrationDTO
        //(
        //    "Frankfurt",
        //    homeTeamId,
        //    awayTeamId,
        //    new DateTime(2024, 12, 06, 09, 30, 49, 312)
        //);

        //// act

        //var loginResult = await Client!.PostAsync("api/v1/login", content);
        //var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        //var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        //var response = await Client!.PostAsync($"api/v1/games/register", new StringContent(JsonConvert.SerializeObject(gameRegistrationDto), Encoding.UTF8, "application/json"));

        ////assert

        //Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //Assert.NotNull(response);
    }

    [Fact]
    public async Task DeleteGameAsync_WithUnauthorizedCoachId_ReturnsBadRequest()
    {
        //// arrange

        //LoginDTO loginDto = new() { Username = "ottis@epost.no", Password = "O1tesen#" };
        //var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        //StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        //var homeTeamId = new TeamId(Guid.Parse("b2d84d4e-921c-4c17-af43-18d13b105004"));
        //var awayTeamId = new TeamId(Guid.Parse("b01b6b08-2f43-4be5-b40b-7b9fd2d3d009"));

        //var gameRegistrationDto = new GameRegistrationDTO
        //(
        //    "Frankfurt",
        //    homeTeamId,
        //    awayTeamId,
        //    new DateTime(2024, 12, 06, 09, 30, 49, 312)
        //);

        //// act

        //var loginResult = await Client!.PostAsync("api/v1/login", content);
        //var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        //var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        //var response = await Client!.PostAsync($"api/v1/games/register", new StringContent(JsonConvert.SerializeObject(gameRegistrationDto), Encoding.UTF8, "application/json"));

        ////assert

        //Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //Assert.NotNull(response);
    }

}
