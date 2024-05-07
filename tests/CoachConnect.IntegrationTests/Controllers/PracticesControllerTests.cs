using CoachConnect.BusinessLayer.DTOs.Games;
using CoachConnect.BusinessLayer.DTOs.Login;
using CoachConnect.BusinessLayer.DTOs.Practice;
using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.DataAccess.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace CoachConnect.IntegrationTests.Controllers;

public class PracticesControllerTests : BaseIntegrationTests
{
    public PracticesControllerTests(CoachConnectWebAppFactory factory) 
     : base(factory)
    {   
    }


    [Fact]
    public async Task GetAllPracticesAsync_default_ReturnStatusCodeOK200()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "elsrøn@yyoyo.no", Password = "R1nningen#" }; // User
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


        var response = await Client!.GetAsync($"api/v1/practices");
        var responseData = await response.Content.ReadAsStringAsync();
        var practiceDtos = System.Text.Json.JsonSerializer.Deserialize<List<PracticeResponse>>(responseData);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(practiceDtos);
        Assert.Equal(10, practiceDtos.ToList().Count);

    }

    [Fact]
    public async Task GetAllPracticesAsync_NoLogin_ReturnStatusCodeUnAuthorized401()
    {
        // arrange and act

        var response = await Client!.GetAsync($"api/v1/practices");

        // assert

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

    }

    

    [Fact]
    public async Task CreatePracticeAsync_CoachAddPractice_returnStatusCodeOK200()
    {
        // arrange
        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#" }; // Coach - Access to create practice
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var practiceRegistrationDto = new PracticeRequest
        (
            "Norge",
            new DateTime(2024, 05, 25)
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Client.PostAsync("api/v1/practices/register", new StringContent(JsonConvert.SerializeObject(practiceRegistrationDto), Encoding.UTF8, "application/json"));
        var registeredPractice = JsonConvert.DeserializeObject<PracticeResponse>(await response.Content.ReadAsStringAsync());

        // assert            

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(registeredPractice);
        Assert.Equal(practiceRegistrationDto.Location, registeredPractice.Location);
        Assert.Equal(practiceRegistrationDto.PracticeDate, registeredPractice.PracticeDate);

    }

    [Fact]
    public async Task CreatePracticeAsync_NoLogin_returnStatusCodeUnAuthorized401()
    {
        // Arrange

        var practiceRegistrationDto = new PracticeRequest
        (
            "Norge",
            new DateTime(2024, 05, 25)
        );

        // act

        var response = await Client.PostAsync("api/v1/practices/register", new StringContent(JsonConvert.SerializeObject(practiceRegistrationDto), Encoding.UTF8, "application/json"));

        // assert            

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

    }

    [Fact]
    public async Task CreatePracticeAsync_UserAddPractice_returnStatusCodeForbidden403()
    {
        // arrange
        LoginDTO loginDto = new() { Username = "elsrøn@yyoyo.no", Password = "R1nningen#" }; // User - No Access to create practice
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var practiceRegistrationDto = new PracticeRequest
        (
            "Norge",
            new DateTime(2024, 05, 25)
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Client.PostAsync("api/v1/practices/register", new StringContent(JsonConvert.SerializeObject(practiceRegistrationDto), Encoding.UTF8, "application/json"));

        // assert            

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

    }


    [Fact]
    public async Task UpdatePracticeByIdAsync_WithValidPracticeId_WithValidCoachId_ReturnsStatusOK200()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var practiceId = new PracticeId(new Guid("2f042e86-d75e-4591-a810-aca808726604"));

        var practiceUpdate = new PracticeUpdate
        (
            "Stockholm",
            new DateTime(2024, 06, 01, 13, 00, 49, 312)
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.PutAsync($"api/v1/practices/{practiceId.practiceId}", new StringContent(JsonConvert.SerializeObject(practiceUpdate), Encoding.UTF8, "application/json"));
        var responseDTO = await response.Content.ReadFromJsonAsync<PracticeUpdate>();

        //assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(responseDTO);
        Assert.Equal(practiceUpdate.Location, responseDTO.Location);
        Assert.Equal(practiceUpdate.PracticeDate, responseDTO.PracticeDate);
    }

    [Fact]
    public async Task UpdatePracticeByIdAsync_WithValidPracticeId_WithNotValidLogin_ReturnsStatusUnAuthorized401()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "koppen@gmail.com", Password = "E1derkopp#123" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var practiceId = new PracticeId(new Guid("2f042e86-d75e-4591-a810-aca808726604"));

        var practiceUpdate = new PracticeUpdate
        (
            "Stockholm",
            new DateTime(2024, 06, 01, 13, 00, 49, 312)
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);

        var response = await Client!.PutAsync($"api/v1/practices/{practiceId.practiceId}", new StringContent(JsonConvert.SerializeObject(practiceUpdate), Encoding.UTF8, "application/json"));
        //assert

        Assert.Equal(HttpStatusCode.Unauthorized, loginResult.StatusCode);
    }

}