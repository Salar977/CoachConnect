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

        LoginDTO dto = new LoginDTO { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize<LoginDTO>(dto);

        // act

        StringContent content = new StringContent(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");
        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var token = await loginResult.Content.ReadAsStringAsync();
        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // assert

        Assert.Equal(HttpStatusCode.OK, loginResult.StatusCode);
        Assert.NotNull(loginResult);
        Assert.NotNull(loginResult);
    }
}