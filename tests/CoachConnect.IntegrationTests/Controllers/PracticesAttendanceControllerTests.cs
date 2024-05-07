using CoachConnect.BusinessLayer.DTOs.GameAttendances;
using CoachConnect.BusinessLayer.DTOs.Login;
using CoachConnect.BusinessLayer.DTOs.PracticeAttendanceDtos;
using CoachConnect.DataAccess.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace CoachConnect.IntegrationTests.Controllers;

public class PracticesAttendanceControllerTests : BaseIntegrationTests
{
    public PracticesAttendanceControllerTests(CoachConnectWebAppFactory factory) : base(factory)
    {
        
    }

    [Fact]
    public async Task RegisterPracticeAttendanceAsync_AttendanceAlreadyExistsForPractice_ReturnsBadRequest400()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var practiceId = Guid.Parse("2f042e86-d75e-4591-a810-aca808726b02");
        var PlayerId = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        var practiceAttendanceRequest = new PracticeAttendanceRequest
        (
         practiceId,
         PlayerId
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PostAsync($"api/v1/practiceattendances/register",
            new StringContent(JsonConvert.SerializeObject(practiceAttendanceRequest), Encoding.UTF8, "application/json"));

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

        var practiceId = Guid.Parse("2f042e86-d75e-4591-a810-aca808726b02");
        var PlayerId = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        var practiceAttendanceRequest = new PracticeAttendanceRequest
        (
         practiceId,
         PlayerId
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PostAsync($"api/v1/practiceattendances/register", new StringContent(JsonConvert.SerializeObject(practiceAttendanceRequest), Encoding.UTF8, "application/json"));

        //assert

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task DeletePracticeAttendanceAsync_WithValidCoachLogin_ReturnsOK200()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "sol@epost.com", Password = "S1lskjær#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var practiceAttendanceId = Guid.Parse("9615514a-c2f8-46fd-a547-ab5c1fc76021");

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.DeleteAsync($"api/v1/practiceattendances/{practiceAttendanceId}");
        var responseDTO = await response.Content.ReadFromJsonAsync<PracticeAttendanceResponse>();

        //assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(responseDTO);
    }

    [Fact]
    public async Task DeletePracticeAttendanceAsync_UserLogin_ReturnsForbidden()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "sara@abc.no", Password = "A1dersen#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var practiceAttendanceId = Guid.Parse("9815514a-c2f8-46fd-a547-ab5c1fc76888");

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.DeleteAsync($"api/v1/practiceattendances/{practiceAttendanceId}");

        //assert

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        Assert.NotNull(response);
    }
}