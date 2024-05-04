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
using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.DTOs.Users;
using CoachConnect.BusinessLayer.DTOs.Games;
using System.Net.Http.Json;

namespace CoachConnect.IntegrationTests.Controllers;
public class GamesControllerTests : BaseIntegrationTests
{
    public GamesControllerTests(CoachConnectWebAppFactory factory)
    : base(factory)
    {
    }

    [Fact]
    public async Task GetGamesAsync_DefaultPageSizeAndEmptyQuery_ReturnStatusOKAndGames()
    {
        // arrange

        LoginDTO loginDto = new LoginDTO { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new StringContent(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

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
    public async Task GetGameByIdAsync_WithValidId_Returns_StatusOKAndGame()
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
}
