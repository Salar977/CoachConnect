using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.DTOs.Users;
using System.Net;

namespace CoachConnect.IntegrationTests.Controllers;

public class LoginControllerTests :BaseIntegrationTests
{
    public LoginControllerTests(CoachConnectWebAppFactory factory)
       : base(factory)
    {
    }

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ReturnsToken()
    {
        // arrange

        LoginDTO LoginDto = new() { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize<LoginDTO>(LoginDto);

        // act

        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");
        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var token = await loginResult.Content.ReadAsStringAsync();

        // assert

        Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
        Assert.NotNull(loginResult);
        Assert.NotNull(token);
    }

    [Fact]
    public async Task LoginAsync_WithIncorrectCredentials_ReturnsUnathorized()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "thisuserdoesnotexist@gmail.com", Password = "doesnotexist" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize<LoginDTO>(loginDto);

        // act

        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");
        var loginResult = await Client!.PostAsync("api/v1/login", content);

        // assert

        Assert.Equal(HttpStatusCode.Unauthorized, loginResult.StatusCode);
    }

    [Fact]
    public async Task LoginAsync_WithIncorrectPassword_ReturnsUnauthorized()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "quyen123@hotmail.com", Password = "incorrectpassword" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize<LoginDTO>(loginDto);

        // act

        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");
        var loginResult = await Client!.PostAsync("api/v1/login", content);

        // assert

        Assert.Equal(HttpStatusCode.Unauthorized, loginResult.StatusCode);
    }
}