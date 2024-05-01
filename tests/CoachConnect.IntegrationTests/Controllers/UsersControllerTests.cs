using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
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
    public async Task Get_Users_Async_DefaultPageSize_Empty_Query_Return_Status_OK_And_Users() 
    {
        // arrange

        //LoginDTO dto = new LoginDTO { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        //var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize<LoginDTO>(dto);

        var userQuery = new UserQuery();

        // act

        //StringContent content = new StringContent(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");
        //var loginResult = await Client!.PostAsync("api/v1/login", content);
        //var token = await loginResult.Content.ReadAsStringAsync();
        //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync("api/v1/users");
        var userDtos = await UserService!.GetAllAsync(userQuery);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(userDtos);
        Assert.Equal(10, userDtos.ToList().Count);
    }

    [Fact]
    public async Task Get_Users_By_LastName_Using_Query_Async_DefaultSize_10_Return_Users()
    {
        // arrange

        //LoginDTO dto = new LoginDTO { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        //var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize<LoginDTO>(dto);

        var userQuery = new UserQuery { LastName = "Andersen" };

        // act

        //StringContent content = new StringContent(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");
        //var loginResult = await Client!.PostAsync("api/v1/login", content);
        //var token = await loginResult.Content.ReadAsStringAsync();
        //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync("/api/v1/users?LastName=Andersen");
        var userDtos = await UserService!.GetAllAsync(userQuery);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(userDtos);
        Assert.Equal(2, userDtos.ToList().Count);
    }

    [Fact]
    public async Task Get_User_By_Id_Async_Returns_Status_OK_And_User()
    {
        // arrange

        //LoginDTO dto = new LoginDTO { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        //var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize<LoginDTO>(dto);

        var guid = new Guid("22222222-2222-2222-2222-222222222222");
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

        //StringContent content = new StringContent(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");
        //var loginResult = await Client!.PostAsync("api/v1/login", content);
        //var token = await loginResult.Content.ReadAsStringAsync();
        //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync("api/v1/users/22222222-2222-2222-2222-222222222222");
        var userDto = await UserService!.GetByIdAsync(guid);

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
    public async Task Get_User_By_Email_Async_Return_User_Data()
    {
        // arrange

        //LoginDTO dto = new LoginDTO { Username = "quyen123@hotmail.com", Password = "Q1yenAdmin#" };
        //var jsonLoginDto = System.Text.Json.JsonSerializer.Serialize<LoginDTO>(dto);

        var userId = new UserId(new Guid("22222222-2222-2222-2222-222222222222"));
        var email = "mariah@yahoo.com";

        User user = new()
        {
            Id = userId,
            FirstName = "Maria",
            LastName = "Mariah",
            PhoneNumber = "99999999",
            Email = email,
            HashedPassword = "$2a$11$2nb9L2C0b8QLyU5xRdpqtu7/Qw89vF7aLl0yWQ/dnMZ8M2N/cDBhK",
            Salt = "$2a$11$2nb9L2C0b8QLyU5xRdpqtu",
            Created = new DateTime(2024, 02, 22, 19, 54, 51),
            Updated = new DateTime(2024, 02, 22, 19, 54, 51),

        };

        // act

        //StringContent content = new StringContent(jsonLoginDto, System.Text.Encoding.UTF8, "application/json");
        //var loginResult = await Client!.PostAsync("api/v1/login", content);
        //var token = await loginResult.Content.ReadAsStringAsync();
        //Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await Client!.GetAsync("api/v1/users/mariah@yahoo.com");
        var userDto = await UserService!.GetByEmailAsync(email);

        // assert

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(userDto);
        Assert.Equal(user.Id, userDto.Id);
        Assert.Equal(user.FirstName, userDto.FirstName);
        Assert.Equal(user.LastName, userDto.LastName);
        Assert.Equal(user.PhoneNumber, userDto.PhoneNumber);
        Assert.Equal(user.Email, userDto.Email);
    }
}   