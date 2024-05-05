using CoachConnect.BusinessLayer.DTOs.Login;
using CoachConnect.BusinessLayer.DTOs.Practice;
using System.Net;

namespace CoachConnect.IntegrationTests.Controllers;

public class PracticesControllerTests : BaseIntegrationTests
{
    public PracticesControllerTests(CoachConnectWebAppFactory factory) 
     : base(factory)
    {   
    }

    [Fact]
    public async Task GetAllPracticesAsync_default_ReturnStatusCode200()
    {
        // arrange

        LoginDTO loginDto = new() { Username = "elsrøn@yyoyo.no", Password = "R1nningen#" };
        var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize(loginDto);
        StringContent content = new(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");

        // act

        var loginResult = await Client!.PostAsync("api/v1/login", content);
        var tokenResponse = await loginResult.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(tokenResponse).RootElement.GetProperty("token").GetString();

        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync("api/v1/practices");
        var responseData = await response.Content.ReadAsStringAsync();
        var practiceDtos = System.Text.Json.JsonSerializer.Deserialize<List<PracticeResponse>>(responseData);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(practiceDtos);
        Assert.Equal(10, practiceDtos.ToList().Count);

    }
}