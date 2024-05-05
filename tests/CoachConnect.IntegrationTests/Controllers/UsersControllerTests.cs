using CoachConnect.BusinessLayer.DTOs.Login;
using CoachConnect.BusinessLayer.DTOs.Users;
using CoachConnect.DataAccess.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace CoachConnect.IntegrationTests.Controllers;

public class UsersControllerTests : BaseIntegrationTests
{
    public UsersControllerTests(CoachConnectWebAppFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task GetUsersAsync_DefaultPageSizeAndEmptyQuery_ReturnStatusOKAndUsers() 
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

        var response = await Client!.GetAsync("api/v1/users");
        var responseData = await response.Content.ReadAsStringAsync();
        var userDtos = System.Text.Json.JsonSerializer.Deserialize<List<UserDTO>>(responseData);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(userDtos);
        Assert.Equal(10, userDtos.ToList().Count);
    }

    [Fact]
    public async Task GetUsersByLastNameAsync_UsingValidQuery_ReturnsOKAndDefaultSizeListUsers()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var query = "?LastName=Andersen";

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync($"/api/v1/users{query}");
        var responseData = await response.Content.ReadAsStringAsync();
        var userDtos = System.Text.Json.JsonSerializer.Deserialize<List<UserDTO>>(responseData);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(userDtos);
        Assert.Equal(2, userDtos.ToList().Count);
    }

    [Fact]
    public async Task GetUserByEmailAsync_WithValidQuery_ReturnsOKAndUser()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var query = "?Email=emma123%40hotmail.com";

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync($"api/v1/users{query}");
        var responseData = await response.Content.ReadAsStringAsync();
        var userDtos = System.Text.Json.JsonSerializer.Deserialize<List<UserDTO>>(responseData);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(userDtos);
    }

    [Fact]
    public async Task GetUserByIdAsync_WithValidId_Returns_StatusOKAndUser()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var userId = new UserId(new Guid("22222222-2222-2222-2222-222222222222"));

        User user = new()
        {
            Id = userId,
            FirstName = "Maria",
            LastName = "Mariah",
            PhoneNumber = "99999999",
            Email = "mariah@yahoo.com",
            HashedPassword = "$2a$11$2nb9L2C0b8QLyU5xRdpqtu7/Qw89vF7aLl0yWQ/dnMZ8M2N/cDBhK",
            Salt = "$2a$11$2nb9L2C0b8QLyU5xRdpqtu",
            Created = new DateTime(2024, 02, 22, 19, 54, 51),
            Updated = new DateTime(2024, 02, 22, 19, 54, 51),
        };

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync($"api/v1/users/{userId.userId}");
        var userDto = await response.Content.ReadFromJsonAsync<UserDTO>();

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(userDto);
        Assert.Equal(user.Id, userDto.Id);
        Assert.Equal(user.FirstName, userDto.FirstName);
        Assert.Equal(user.LastName, userDto.LastName);
        Assert.Equal(user.PhoneNumber, userDto.PhoneNumber);
        Assert.Equal(user.Email, userDto.Email);
    }

    [Fact]
    public async Task RegisterUserAsync_WithValidUserData_ReturnsStatusOKAndRegisteredUser() 
    {
        // arrange

        var UserRegistrationDTO = new UserRegistrationDTO
        (
          "Jan",
          "Jensen",
          "21212121",
          "J1nsen###",
          "jan@gmail.com"
        );

        // act

        var response = await Client.PostAsync("api/v1/users/register", new StringContent(JsonConvert.SerializeObject(UserRegistrationDTO), Encoding.UTF8, "application/json"));
        var registeredUser = JsonConvert.DeserializeObject<UserDTO>(await response.Content.ReadAsStringAsync());

        // assert            

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(registeredUser);
        Assert.Equal(UserRegistrationDTO.FirstName, registeredUser.FirstName);
        Assert.Equal(UserRegistrationDTO.LastName, registeredUser.LastName);
        Assert.Equal(UserRegistrationDTO.PhoneNumber, registeredUser.PhoneNumber);
        Assert.Equal(UserRegistrationDTO.Email, registeredUser.Email);
    }
    
    [Fact]
    public async Task UpdateUserAsync_WithValidUserId_ReturnsStatusOKAndUpdatedUser()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "sara@abc.no", Password = "A1dersen#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        var userId = "20065784-cdb9-465a-a439-6a627c448ca8";
        var userUpdateDto = new UserCoachUpdateDTO
        (
          "Sara",
          "Andersen",
          "31313131",
          "sara@abc.no"
        );

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client.PutAsync($"api/v1/users/{userId}", new StringContent(JsonConvert.SerializeObject(userUpdateDto), Encoding.UTF8, "application/json"));
        var updatedUserJson = await response.Content.ReadAsStringAsync();
        var updatedUserDto = JsonConvert.DeserializeObject<UserCoachUpdateDTO>(updatedUserJson);

        // assert        

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(updatedUserDto);
        Assert.Equal(userUpdateDto.FirstName, updatedUserDto.FirstName);
        Assert.Equal(userUpdateDto.LastName, updatedUserDto.LastName);
        Assert.Equal(userUpdateDto.PhoneNumber, updatedUserDto.PhoneNumber);
        Assert.Equal(userUpdateDto.Email, updatedUserDto.Email);
    }

    [Fact]
    public async Task DeleteUserAsync_WithValidUserId_ReturnsStatusOKAndDeletedUser() 
    {
        // arrange

        var userId = "f15a1513-eb40-4ca3-b8bb-c06959e1d6b5";    

        LoginDTO loginDto = new() { Username = "jens@gmail.com", Password = "J1nsen#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client.DeleteAsync($"api/v1/users/{userId}");
        var deletedUser = await Client!.GetAsync($"api/v1/users/{userId}");

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(deletedUser);
    }

    [Fact]
    public async Task DeleteUserAsync_WithNonValidUserId_ReturnsUnauthorized()
    {
        // arrange

        var userId = "d9e4e229-7738-4d26-822d-1b13fb1052c9";

        LoginDTO loginDto = new() { Username = "jens@gmail.com", Password = "J1nsen#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client.DeleteAsync($"api/v1/users/{userId}");

        // assert

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}