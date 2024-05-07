using CoachConnect.BusinessLayer.DTOs.Login;
using CoachConnect.BusinessLayer.DTOs.Players;
using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.DataAccess.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.IntegrationTests.Controllers;
public class TeamControllerTests : BaseIntegrationTests
{
    public TeamControllerTests(CoachConnectWebAppFactory factory)
        : base(factory)
    { 

    }

    [Fact]
    public async Task GetTeamsAsync_DefaultPageSizeAndEmptyQuery_ReturnStatusOKAndTeams()
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

        var response = await Client!.GetAsync("api/v1/teams");
        var responseData = await response.Content.ReadAsStringAsync();
        var teamResp = System.Text.Json.JsonSerializer.Deserialize<List<TeamResponse>>(responseData);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(teamResp);
        Assert.Equal(10, teamResp.ToList().Count);
    }

    [Fact]
    public async Task GetTeamByTeamCityAsync_UsingValidQuery_ReturnsOKAndDefaultSizeListTeam()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var query = "?TeamCity=Boston";


        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync($"api/v1/teams{query}");
        var responseData = await response.Content.ReadAsStringAsync();
        var teamResp = System.Text.Json.JsonSerializer.Deserialize<List<TeamResponse>>(responseData);
        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(teamResp);
    }

    [Fact]
    public async Task GetTeamByIdAsync_WithValidId_Returns_StatusOkAndTeam()
    {

        // arrange

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var teamId = new TeamId(new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4020"));


        Team team = new()
        {
            Id = teamId,
            TeamCity = "Boston",
            TeamName = "Warriors",
            Created = new DateTime(2024, 04, 17, 13, 00, 49, 312),
            Updated = new DateTime(2024, 04, 17, 13, 00, 49, 312),
        };

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync($"api/v1/teams/{teamId.teamId}");
        var teamResp = await response.Content.ReadFromJsonAsync<TeamResponse>();

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(teamResp);
        Assert.Equal(team.TeamCity, teamResp.TeamCity);
        Assert.Equal(team.TeamName, teamResp.TeamName);

    }

    [Fact]
    public async Task RegisterTeamAsync_WithValidTeamData_ReturnsStatusOKAndRegisteredTeam()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var teamReq = new TeamRequest
        (
          "Madrid",
          "Madrid Hunters",
          Guid.Parse("0a95b9b1-6fb7-42a7-8333-56e649a48fe7")
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client.PostAsync("api/v1/teams/register", new StringContent(JsonConvert.SerializeObject(teamReq), Encoding.UTF8, "application/json"));
        var registeredTeam = JsonConvert.DeserializeObject<TeamResponse>(await response.Content.ReadAsStringAsync());

        // assert            

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(registeredTeam);
        Assert.Equal(teamReq.TeamCity, registeredTeam.TeamCity);
        Assert.Equal(teamReq.TeamName, registeredTeam.TeamName);
    }
}
