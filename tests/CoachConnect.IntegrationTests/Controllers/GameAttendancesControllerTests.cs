using CoachConnect.BusinessLayer.DTOs.GameAttendances;
using CoachConnect.BusinessLayer.DTOs.Games;
using CoachConnect.BusinessLayer.DTOs.Login;
using CoachConnect.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Renci.SshNet.Security.Cryptography;
using System.Net;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;

namespace CoachConnect.IntegrationTests.Controllers;

public class GameAttendancesControllerTests : BaseIntegrationTests
{
    public GameAttendancesControllerTests(CoachConnectWebAppFactory factory)
    : base(factory)
    {
    }

    [Fact]
    public async Task RegisterGameAttendanceAsync_WithValidCoachId_WherePlayerIsOnPlayingTeam_AndAttendanceForPlayerDoesNotAlreadyExist_ReturnsOKAndRegisteredGameAttendance()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var gameId = new GameId(Guid.Parse("2f042e86-d75e-4591-a810-aca808726b02"));
        var PlayerId = new PlayerId(Guid.Parse("12345678-1234-1234-1234-123456789abc"));

        var gameAttendanceRegistrationDTO = new GameAttendanceRegistrationDTO
        (
         gameId,
         PlayerId
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PostAsync($"api/v1/gameattendances/register", new StringContent(JsonConvert.SerializeObject(gameAttendanceRegistrationDTO), Encoding.UTF8, "application/json"));
        var responseDTO = await response.Content.ReadFromJsonAsync<GameAttendanceRegistrationDTO>();

        //assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(responseDTO);
        Assert.Equal(gameAttendanceRegistrationDTO.GameId, responseDTO.GameId);
        Assert.Equal(gameAttendanceRegistrationDTO.PlayerId, responseDTO.PlayerId);
    }
    
    [Fact]
    public async Task RegisterGameAttendanceAsync_WithValidCoachId_WherePlayerIsOnPlayingTeam_WhereAttendanceForPlayerAlreadyExists_ReturnsBadRequest()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var gameId = new GameId(Guid.Parse("2f042e86-d75e-4591-a810-aca808cdd0d9"));
        var PlayerId = new PlayerId(Guid.Parse("12345678-1234-1234-1234-123456789abc"));

        var gameAttendanceRegistrationDTO = new GameAttendanceRegistrationDTO
        (
         gameId,
         PlayerId
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PostAsync($"api/v1/gameattendances/register", new StringContent(JsonConvert.SerializeObject(gameAttendanceRegistrationDTO), Encoding.UTF8, "application/json"));

        //assert

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task RegisterGameAttendanceAsync_WhereCoachRegistering_IsNotCoachOfEitherTeamPlaying_ReturnsBadRequest()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var gameId = new GameId(Guid.Parse("2f042e86-d75e-4591-a810-aca808727777"));
        var PlayerId = new PlayerId(Guid.Parse("12345678-1234-1234-1234-123456789abc"));

        var gameAttendanceRegistrationDTO = new GameAttendanceRegistrationDTO
        (
         gameId,
         PlayerId
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PostAsync($"api/v1/gameattendances/register", new StringContent(JsonConvert.SerializeObject(gameAttendanceRegistrationDTO), Encoding.UTF8, "application/json"));

        //assert

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task RegisterGameAttendanceAsync_WithValidCoachId_WherePlayerIdIsNotValid_AsPlayerDoesNotPlayForTheRegisteringCoachTeam_ReturnsBadRequest()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var gameId = new GameId(Guid.Parse("2f042e86-d75e-4591-a810-aca808727777"));
        var PlayerId = new PlayerId(Guid.Parse("87654321-3456-3456-3456-123456789155"));

        var gameAttendanceRegistrationDTO = new GameAttendanceRegistrationDTO
        (
         gameId,
         PlayerId
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PostAsync($"api/v1/gameattendances/register", new StringContent(JsonConvert.SerializeObject(gameAttendanceRegistrationDTO), Encoding.UTF8, "application/json"));

        //assert

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task RegisterGameAttendanceAsync_WithValidCoachId_WhereGameDoesNotExist_ReturnsBadRequest()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var gameId = new GameId(Guid.Parse("2f042e86-d75e-4591-a810-aaaaaaaaaaaa"));
        var PlayerId = new PlayerId(Guid.Parse("87654321-3456-3456-3456-123456789155"));

        var gameAttendanceRegistrationDTO = new GameAttendanceRegistrationDTO
        (
         gameId,
         PlayerId
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PostAsync($"api/v1/gameattendances/register", new StringContent(JsonConvert.SerializeObject(gameAttendanceRegistrationDTO), Encoding.UTF8, "application/json"));

        //assert

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task DeleteGameAttendanceAsync_WithValidCoachRole_WhereCoachIsCoachForThePlayerHavingTheirAttendanceDeleted_ReturnsOKAndDeletedGameAttendance()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "sol@epost.com", Password = "S1lskjær#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var gameAttendanceId = new GameAttendanceId(new Guid("aa15514a-c2f8-46fd-a547-ab5c1fc76e14"));

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.DeleteAsync($"api/v1/gameattendances/{gameAttendanceId.gameAttendanceId}");
        var responseDTO = await response.Content.ReadFromJsonAsync<GameAttendanceDTO>();

        //assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(responseDTO);
    }

    [Fact]
    public async Task DeleteGameAttendanceAsync_WithValidCoachRole_WhereCoachIsNotCoachForThePlayerHavingTheirAttendanceDeleted_ReturnsBadRequest()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "sol@epost.com", Password = "S1lskjær#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var gameAttendanceId = new GameAttendanceId(new Guid("9815514a-c2f8-46fd-a547-ab5c1fc76888"));

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.DeleteAsync($"api/v1/gameattendances/{gameAttendanceId.gameAttendanceId}");

        //assert

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task DeleteGameAttendanceAsync_MissingCoachRoleInToken_ReturnsForbidden()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "sara@abc.no", Password = "A1dersen#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var gameAttendanceId = new GameAttendanceId(new Guid("9815514a-c2f8-46fd-a547-ab5c1fc76888"));

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.DeleteAsync($"api/v1/gameattendances/{gameAttendanceId.gameAttendanceId}");

        //assert

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        Assert.NotNull(response);
    }
}