using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        //LoginDTO dto = new LoginDTO { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        //var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize<LoginDTO>(dto);

        var playerQuery = new PlayerQuery();

        // act

        //StringContent content = new StringContent(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");
        //var loginResult = await Client!.PostAsync("api/v1/login", content);
        //var token = await loginResult.Content.ReadAsStringAsync();
        //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync("api/v1/players");
        //var gameDtos = await PlayerService!.GetAllAsync(playerQuery);

        // assert
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //Assert.NotNull(gameDtos);
        //Assert.Equal(10, gameDtos.ToList().Count);
    }

    [Fact]
    public async Task GetGameByIdAsync_WithValidId_Returns_StatusOKAndGame()
    {
        // arrange

        var guid = new Guid("2f042e86-d75e-4591-a810-aca808725555");
        var playerId = new PlayerId(new Guid("2f042e86-d75e-4591-a810-aca808725555"));

        var homeCoachId = new CoachId(Guid.Parse("a3b2a7e5-b0e2-40e2-a42d-69e10a22d011"));
        var awayCoachId = new CoachId(Guid.Parse("b01b6b08-2f43-4be5-b40b-7b9fd2d3d009"));

        Player player = new()
        {
            Id = playerId,
            FirstName = "Kristian",
            LastName = "Walin",
            Created = new DateTime(2024, 04, 17, 13, 00, 49, 312),
        };

        // act

        var response = await Client!.GetAsync("api/v1/players/2f042e86-d75e-4591-a810-aca808725555");
        //var playerDto = await PlayerService!.GetByIdAsync(guid);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //Assert.NotNull(playerDto);
        //Assert.Equal(player.Id, playerDto.Id);
        //Assert.Equal(player.FirstName, playerDto.FirstName);
        //Assert.Equal(player.LastName, playerDto.LastName);
    }
}
