using CoachConnect.BusinessLayer.DTOs;
using System.Net;
using System.Text;

namespace CoachConnect.IntegrationTests.Controllers;

public class UsersControllerTests : BaseIntegrationTests
{
    public UsersControllerTests(CoachConnectWebAppFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task GetUsersAsync_DefaultPageSize_ReturnData() 
    {
        // arrange

        //LoginDTO dto = new LoginDTO { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        //var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize<LoginDTO>(dto);

        //// act
        //// login > return token
        //// add token to http-header for Client
        ////Client.DefaultRequestHeaders("Bearer", "...")
        //StringContent content = new StringContent(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");
        //var loginResult = await Client!.PostAsync("api/v1/login", content);
        //// token
        //var token = await loginResult.Content.ReadAsStringAsync();

        //// setter token
        //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync("api/v1/users");

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}   