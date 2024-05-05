using CoachConnect.BusinessLayer.DTOs.Coach;
using CoachConnect.BusinessLayer.DTOs.Login;
using CoachConnect.BusinessLayer.DTOs.Users;
using CoachConnect.DataAccess.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace CoachConnect.IntegrationTests.Controllers;

public class CoachesControllerTests : BaseIntegrationTests
{
    public CoachesControllerTests(CoachConnectWebAppFactory factory)
     : base(factory)
    {
    }

    [Fact]
    public async Task GetCoachesAsync_DefaultPageSizeAndEmptyQuery_ReturnStatusOKAndCoaches()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "sol@epost.com", Password = "S1lskjær#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync("api/v1/coaches");
        var responseData = await response.Content.ReadAsStringAsync();
        var coachDtos = System.Text.Json.JsonSerializer.Deserialize<List<CoachDTO>>(responseData);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(coachDtos);
        Assert.Equal(10, coachDtos.ToList().Count);
    }

    [Fact]
    public async Task GetCoachesByFirstNameAsync_UsingValidQuery_ReturnsOKAndDefaultSizeListCoaches()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "sol@epost.com", Password = "S1lskjær#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var query = "?FirstName=Lisa";

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync($"/api/v1/coaches{query}");
        var responseData = await response.Content.ReadAsStringAsync();
        var coachesDtos = System.Text.Json.JsonSerializer.Deserialize<List<CoachDTO>>(responseData);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(coachesDtos);
        Assert.Single(coachesDtos.ToList());
    }

    [Fact]
    public async Task GetCoachByEmailAsync_WithValidQuery_ReturnsOKAndCoach()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "sol@epost.com", Password = "S1lskjær#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var query = "?Email=mike%40hotmail.com";

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync($"api/v1/coaches{query}");
        var responseData = await response.Content.ReadAsStringAsync();
        var coachDto = System.Text.Json.JsonSerializer.Deserialize<List<UserDTO>>(responseData);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(coachDto);
    }

    [Fact]
    public async Task GetCoachByIdAsync_AsAdmin_Returns_StatusOKAndUser()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var coachId = new CoachId(new Guid("b1f5ef4e-c48c-4db3-a10b-07d65da0614b"));

        Coach coach = new()
        {
            Id = coachId,
            FirstName = "Chris",
            LastName = "Brown",
            PhoneNumber = "87654321",
            Email = "chris@yahoo.com",
            HashedPassword = "$2a$11$2.E9V0VRslIqC3S1G5ld9O1cMnH4LWCeX9kQsDhPWNO2dJxkG3/ai",
            Salt = "$2a$11$2.E9V0VRslIqC3S1G5ld9O",
            Created = new DateTime(2024, 03, 06, 14, 23, 24),
            Updated = new DateTime(2024, 03, 06, 14, 23, 24),
        };

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync($"api/v1/coaches/{coachId.coachId}");
        var coachDto = await response.Content.ReadFromJsonAsync<CoachDTO>();

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(coachDto);
        Assert.Equal(coach.Id, coachDto.Id);
        Assert.Equal(coach.FirstName, coachDto.FirstName);
        Assert.Equal(coach.LastName, coachDto.LastName);
        Assert.Equal(coach.PhoneNumber, coachDto.PhoneNumber);
        Assert.Equal(coach.Email, coachDto.Email);
    }

    [Fact]
    public async Task RegisterCoachAsync_WithValidUserData_ReturnsStatusOKAndRegisteredCoach()
    {
        // arrange

        var CoachRegistrationDTO = new CoachRegistrationDTO
        (
          "Nils",
          "Nilsen",
          "91827364",
          "N1lsen##",
          "nils@abc.com"
        );

        // act

        var response = await Client.PostAsync("api/v1/coaches/register", new StringContent(JsonConvert.SerializeObject(CoachRegistrationDTO), Encoding.UTF8, "application/json"));
        var registeredCoach = JsonConvert.DeserializeObject<UserDTO>(await response.Content.ReadAsStringAsync());

        // assert            

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(registeredCoach);
        Assert.Equal(CoachRegistrationDTO.FirstName, registeredCoach.FirstName);
        Assert.Equal(CoachRegistrationDTO.LastName, registeredCoach.LastName);
        Assert.Equal(CoachRegistrationDTO.PhoneNumber, registeredCoach.PhoneNumber);
        Assert.Equal(CoachRegistrationDTO.Email, registeredCoach.Email);
    }

    [Fact]
    public async Task UpdateCoachesAsync_WithNotValidCoachId_ReturnsUnauthorized()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "ottis@epost.no", Password = "O1tesen#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var coachId = "e00927cb-a616-43d3-a82e-21439469ef13";
        var coachUpdateDto = new UserCoachUpdateDTO
        (
          "Martin",
          "Ottesen",
          "90908776",
          "martin999@abc.no"
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client.PutAsync($"api/v1/coaches/{coachId}", new StringContent(JsonConvert.SerializeObject(coachUpdateDto), Encoding.UTF8, "application/json"));

        // assert        

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCoachAsync_AsAdmin_ReturnsStatusOKAndDeletedCoach()
    {
        // arrange

        var coachId = "a6c5b4d3-2fcb-4e9a-a457-03e9af6d28e6";

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client.DeleteAsync($"api/v1/coaches/{coachId}");
        var deletedCoachJson = await response.Content.ReadAsStringAsync();
        var deletedCoachDto = JsonConvert.DeserializeObject<CoachDTO>(deletedCoachJson);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(deletedCoachDto);
    }

    [Fact]
    public async Task DeleteCoachAsync_WithNonValidUserId_ReturnsForbidden()
    {
        // arrange

        var coachId = "b6c7d8e9-1a2b-3c4d-5e6f-7a8b9c0d1e2f";

        LoginDTO loginDto = new() { Username = "jens@gmail.com", Password = "J1nsen#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client.DeleteAsync($"api/v1/coaches/{coachId}");

        // assert

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        Assert.NotNull(response);
    }
}